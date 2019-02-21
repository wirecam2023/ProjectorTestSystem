#pragma once
#include "afxcmn.h"
#include "afxwin.h"
#include "PackDlg.h"
#include "PloDlg.h"


// CSetIndexDlg 对话框

class CSetIndexDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CSetIndexDlg)

public:
	CSetIndexDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CSetIndexDlg();

// 对话框数据
	enum { IDD = IDD_SETINDEX };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CTreeCtrl m_SetIndex;
	afx_msg void OnBnClickedSelectindex();
	virtual BOOL OnInitDialog();
	CEdit m_IndexType;
	CEdit m_BodyNum;
	CEdit m_SingleBodyNum;
	CEdit m_MainBoardNum;
	afx_msg void OnBnClickedNew();
	CButton m_Save;
	CButton m_Abortion;
	CButton m_Change;
	CButton m_Delete;
	CButton m_Select;
	CButton m_New;
	afx_msg void OnBnClickedSave();
	afx_msg void OnBnClickedAbortion();
	afx_msg void OnBnClickedChange();
	afx_msg void OnTvnSelchangedIndexbuff(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnBnClickedDelete();
	afx_msg void OnBnClickedOk();
public:
	
	virtual void OnCancel();
};
