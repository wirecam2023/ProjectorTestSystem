#pragma once
#include "afxwin.h"


// CInDanNum 对话框

class CInDanNum : public CDialogEx
{
	DECLARE_DYNAMIC(CInDanNum)

public:
	CInDanNum(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CInDanNum();

// 对话框数据
	enum { IDD = IDD_INDANNUM };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	CEdit m_InDanNum;
	virtual BOOL OnInitDialog();
};
