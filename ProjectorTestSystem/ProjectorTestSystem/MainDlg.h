#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CMainDlg 对话框

class CMainDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CMainDlg)

public:
	CMainDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CMainDlg();

// 对话框数据
	enum { IDD = IDD_MAIN };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CListCtrl m_MainList;
	virtual BOOL OnInitDialog();
	afx_msg void OnBnClickedCheckindb();
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	afx_msg void OnBnClickedWritetoexcel();
	void WriteToExcelFunc();
	void  GetCellName(int nRow, int nCol, CString &strName);
	afx_msg void OnBnClickedDeleteselect();
	afx_msg void OnBnClickedDeleteall();
	CEdit m_MainZhiDan;
	CString m_MainBody;
	CString m_MainSingleBody;
	CString m_MainOpticalCode;
	CString m_MMainBoardNum;
	CString m_MainWiredMac;
	CString m_MainWiredlessMac;
	CString TimeTranSlate(_variant_t SourceTime);
	virtual void OnCancel();
	virtual void OnOK();
	CButton m_DeleteAll;
	CButton m_DeleteSelect;
	afx_msg void OnSize(UINT nType, int cx, int cy);
	POINT Mainold;
	CString CheckNull(_variant_t Source);
};
