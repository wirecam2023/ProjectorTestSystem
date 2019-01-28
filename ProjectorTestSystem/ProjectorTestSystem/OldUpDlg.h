#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// COldUpDlg 对话框

class COldUpDlg : public CDialogEx
{
	DECLARE_DYNAMIC(COldUpDlg)

public:
	COldUpDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~COldUpDlg();

// 对话框数据
	enum { IDD = IDD_OLDUP };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CListCtrl m_OldUpList;
	virtual BOOL OnInitDialog();
	CStatic m_OldUp;
	CString m_OldUpStatic;
	//CEdit m_OldEdit;
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	CEdit m_OldUpEdit;
public:
	CString GetTime();

	CString m_OldUpEditVal;
	virtual void OnOK();
	virtual void OnCancel();
};
