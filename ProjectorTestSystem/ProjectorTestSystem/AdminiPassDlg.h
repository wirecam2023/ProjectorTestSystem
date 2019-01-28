#pragma once
#include "afxwin.h"


// CAdminiPassDlg 对话框

class CAdminiPassDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CAdminiPassDlg)

public:
	CAdminiPassDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CAdminiPassDlg();

// 对话框数据
	enum { IDD = IDD_ADMINIPASS };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	virtual BOOL OnInitDialog();
	CEdit m_PassWord;
};
