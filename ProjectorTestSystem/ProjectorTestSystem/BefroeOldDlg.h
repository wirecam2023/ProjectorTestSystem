#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CBefroeOldDlg 对话框

class CBefroeOldDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CBefroeOldDlg)

public:
	CBefroeOldDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CBefroeOldDlg();

// 对话框数据
	enum { IDD = IDD_BEFOREOLD };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CListCtrl m_BeforeOldList;
	virtual BOOL OnInitDialog();
	CStatic m_BeforeOld;
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	CString m_BeforeOldBody;
	CString m_BeforeOldBodyEdit;
public:
	CString GetTime();
	CEdit m_BeforeOldEditContrl;
	virtual void OnOK();
	virtual void OnCancel();
};
