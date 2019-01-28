// OwnerdrawTabCtrl.h : implementation file
//

#include "stdafx.h"
#include "OverDraw.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// COwnerdrawTabCtrl

COwnerdrawTabCtrl::COwnerdrawTabCtrl()
{
}

COwnerdrawTabCtrl::~COwnerdrawTabCtrl()
{
}


BEGIN_MESSAGE_MAP(COwnerdrawTabCtrl, CTabCtrl)
	//{{AFX_MSG_MAP(COwnerdrawTabCtrl)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// COwnerdrawTabCtrl message handlers

void COwnerdrawTabCtrl::DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct)
{
	// TODO: Add your message handler code here and/or call default	

	if (lpDrawItemStruct->CtlType == ODT_TAB)
	{
		CRect rect = lpDrawItemStruct->rcItem;
		INT nTabIndex = lpDrawItemStruct->itemID;
		if (nTabIndex < 0) return;

		TCHAR label[64];
		TC_ITEM tci;
		tci.mask = TCIF_TEXT | TCIF_IMAGE;
		tci.pszText = label;
		tci.cchTextMax = 63;
		GetItem(nTabIndex, &tci);

		CDC* pDC = CDC::FromHandle(lpDrawItemStruct->hDC);
		if (!pDC) return;
		int nSavedDC = pDC->SaveDC();

		//Ìî³ä±³¾°É«
		COLORREF rcBack;
		if (lpDrawItemStruct->itemState & CDIS_SELECTED)
		{
			rcBack = RGB(72, 90, 66);
		}
		else if (lpDrawItemStruct->itemState & (CDIS_DISABLED | CDIS_GRAYED))
		{
			rcBack = RGB(0, 255, 0);
		}
		else
		{
			rcBack = GetSysColor(COLOR_BTNFACE);
		}
		pDC->FillSolidRect(rect, rcBack);

		rect.top += ::GetSystemMetrics(SM_CYEDGE);

		pDC->SetBkMode(TRANSPARENT);

		//»æÖÆÍ¼Æ¬
		CImageList* pImageList = GetImageList();
		if (pImageList && tci.iImage >= 0)
		{
			rect.left += pDC->GetTextExtent(_T(" ")).cx;		// Margin

			// Get height of image so we 
			IMAGEINFO info;
			pImageList->GetImageInfo(tci.iImage, &info);
			CRect ImageRect(info.rcImage);
			INT nYpos = rect.top;

			pImageList->Draw(pDC, tci.iImage, CPoint(rect.left, nYpos), ILD_TRANSPARENT);
			rect.left += ImageRect.Width();
		}

		//»æÖÆ×ÖÌå
		COLORREF txtColor;
		if (lpDrawItemStruct->itemState & CDIS_SELECTED)
		{
			rect.top -= ::GetSystemMetrics(SM_CYEDGE);

			txtColor = RGB(13, 143, 100);
		}
		else if (lpDrawItemStruct->itemState & (CDIS_DISABLED | CDIS_GRAYED))
		{
			txtColor = RGB(128, 128, 128);
		}
		else
		{
			txtColor = GetSysColor(COLOR_WINDOWTEXT);
		}
		pDC->SetTextColor(txtColor);
		pDC->DrawText(label, rect, DT_SINGLELINE | DT_VCENTER | DT_CENTER);

		pDC->RestoreDC(nSavedDC);

	}

}

void COwnerdrawTabCtrl::PreSubclassWindow()
{
	ModifyStyle(0, TCS_OWNERDRAWFIXED);
	CTabCtrl::PreSubclassWindow();
}

BOOL COwnerdrawTabCtrl::Create(LPCTSTR lpszClassName, LPCTSTR lpszWindowName, DWORD dwStyle, const RECT& rect, CWnd* pParentWnd, UINT nID, CCreateContext* pContext)
{
	// TODO: Add your specialized code here and/or call the base class
	dwStyle |= TCS_OWNERDRAWFIXED;
	return CWnd::Create(lpszClassName, lpszWindowName, dwStyle, rect, pParentWnd, nID, pContext);
}
