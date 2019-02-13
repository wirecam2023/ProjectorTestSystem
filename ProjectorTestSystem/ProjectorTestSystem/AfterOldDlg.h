#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CAfterOldDlg 对话框

class CAfterOldDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CAfterOldDlg)

public:
	CAfterOldDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CAfterOldDlg();

// 对话框数据
	enum { IDD = IDD_AFTEROLD };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CListCtrl m_AfetrOldList;
	virtual BOOL OnInitDialog();
	CStatic m_AfterOldNum;
	CEdit m_AfterOldEditContrl;
	CString m_AfterOldBodyEdit;
	CString m_AfterOldBody;
	virtual BOOL PreTranslateMessage(MSG* pMsg);
public:
	CString GetTime();
	virtual void OnOK();
	virtual void OnCancel();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnLvnItemchangedList2(NMHDR *pNMHDR, LRESULT *pResult);
};
