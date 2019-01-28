#pragma once
// OwnerdrawTabCtrl.h : header file
#include "afxcmn.h"
/////////////////////////////////////////////////////////////////////////////
// COwnerdrawTabCtrl window

class COwnerdrawTabCtrl : public CTabCtrl
{
	// Construction
public:
	COwnerdrawTabCtrl();

	// Attributes
public:

	// Operations
public:

	// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(COwnerdrawTabCtrl)
public:
	virtual BOOL Create(LPCTSTR lpszClassName, LPCTSTR lpszWindowName, DWORD dwStyle, const RECT& rect, CWnd* pParentWnd, UINT nID, CCreateContext* pContext = NULL);
protected:
	virtual void PreSubclassWindow();
	//}}AFX_VIRTUAL

	// Implementation
public:
	virtual ~COwnerdrawTabCtrl();

	// Generated message map functions
protected:
	//{{AFX_MSG(COwnerdrawTabCtrl)
	//}}AFX_MSG

	void DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct);

	DECLARE_MESSAGE_MAP()
};
