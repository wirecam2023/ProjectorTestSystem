#pragma once
#include "afxcmn.h"
#include "afxwin.h"



// CPloDlg 对话框

class CPloDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CPloDlg)

public:
	CPloDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CPloDlg();

// 对话框数据
	enum { IDD = IDD_PLO };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	CListCtrl m_PloList;
	CStatic m_PloBodyNum;
	CStatic m_PloSingleBodyNum;
	CStatic m_PloMainBoardNum;
	CButton m_SingleBodyCheck;
	CButton m_MainBoardCheck;
	afx_msg void OnBnClickedSinglebodyCheck();
	CEdit m_PloSingleBodyNumEdit;
	CEdit m_PloMainBoardNumEcit;
	afx_msg void OnBnClickedMainboardCheck();
	BOOL m_SingleBodyNumState;
	BOOL m_MainBoardNumState;
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	CString GetTime();
	CString m_BodyNumVal;
	CString m_SingleBodyNumVal;
	CString m_MainBoardNumVal;
	virtual void OnOK();
	CString m_PloBodyNumStaticVal;
	CString m_SingleBdoyStaticVal;
	CString m_PloMainBoardStaticVal;
	virtual void OnCancel();
	afx_msg void OnDestroy();
	CEdit m_PloBodyNumSub;
	afx_msg void OnSize(UINT nType, int cx, int cy);
};
