#pragma once
#include "afxcmn.h"
#include "afxwin.h"
#include <map>

// CBeforeBrightDlg 对话框
using namespace std;

class CBeforeBrightDlg : public CDialogEx
{
	DECLARE_DYNAMIC(CBeforeBrightDlg)

public:
	CBeforeBrightDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CBeforeBrightDlg();

// 对话框数据
	enum { IDD = IDD_BEFOREBRIGHT };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	CListCtrl m_BeforeBright;
	CStatic m_BeforeBrightNum;
	CEdit m_BrightBodyEdit;
	CEdit m_BrightNessEdit;
	CString m_BrightBodyEditVal;
	CString m_BrightNessEditVal;
	CEdit m_WiredLessEdit;
	CString m_WiredLessEditVal;
	BOOL m_WiredLessEditState;
	CEdit m_WiredEdit;
	CString m_WiredEditVal;
	BOOL m_WiredEditState;
	afx_msg void OnBnClickedExceltosql();
    map<CString, int> ExcelZiDuan;
	CString GetTime();
	CString m_BrightStaticVal;
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	virtual void OnOK();
	virtual void OnCancel();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	CButton m_BrightCheck1;
	CButton m_BrightCheck2;
	CButton m_BrightCheck3;
	CString CheckNull(_variant_t Source);
	CRichEditCtrl m_BrightRich;
	void DeleteNullList(CListCtrl Contrl);
	CString  GetDate();
	CString GetExePath();
	BOOL WritetoTxt(CString sValue);
};
