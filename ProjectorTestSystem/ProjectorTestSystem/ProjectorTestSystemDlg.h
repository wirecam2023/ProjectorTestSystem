
// ProjectorTestSystemDlg.h : 头文件
//

#pragma once
#include "afxcmn.h"
#include "MainDlg.h"
#include "afxwin.h"
#include "PloDlg.h"
#include "BefroeOldDlg.h"
#include "OldUpDlg.h"
#include "OldDownDlg.h"
#include "AfterOldDlg.h"
#include "BeforeBrightDlg.h"
#include "FixDlg.h"
#include "PackDlg.h"
#include "SetIndexDlg.h"
#include "OverDraw.h"


// CProjectorTestSystemDlg 对话框
class CProjectorTestSystemDlg : public CDialogEx
{
// 构造
public:
	CProjectorTestSystemDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_PROJECTORTESTSYSTEM_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	COwnerdrawTabCtrl m_Tab;
	CMainDlg m_Main;
	CPloDlg m_Plo;
	CBefroeOldDlg m_BeforeOld;
	COldUpDlg m_OldUp;
	COldDownDlg m_OldDown;
	CAfterOldDlg m_AfterOld;
	CBeforeBrightDlg m_BeforeBright;
	CFixDlg m_Fix;
	CPackDlg m_Pack;
	CStatusBar m_StateBar;
	CSetIndexDlg SetIndex;
	afx_msg void OnTcnSelchangeTab1(NMHDR *pNMHDR, LRESULT *pResult);
public:
	CString GetExePath();
	void GetConnectStr();
	afx_msg void OnAdminiGetOn();
	afx_msg void OnSetIndex();
	afx_msg void OnLogOut();
	afx_msg void OnExitSystem();
	virtual void OnCancel();
public:
	CString GetTime();
public:
	void ReSize();
	POINT old;
	afx_msg void OnSize(UINT nType, int cx, int cy);
};
