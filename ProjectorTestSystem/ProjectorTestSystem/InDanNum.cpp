// InDanNum.cpp : 实现文件
//

#include "stdafx.h"
#include "ProjectorTestSystem.h"
#include "InDanNum.h"
#include "afxdialogex.h"
#include "ProjectorTestSystemDlg.h"


/*全局变量*/
extern CProjectorTestSystemDlg * ProjectorTestSystemDlg;


// CInDanNum 对话框

IMPLEMENT_DYNAMIC(CInDanNum, CDialogEx)

CInDanNum::CInDanNum(CWnd* pParent /*=NULL*/)
	: CDialogEx(CInDanNum::IDD, pParent)
{

}

CInDanNum::~CInDanNum()
{
}

void CInDanNum::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_DANNUMBER, m_InDanNum);
}


BEGIN_MESSAGE_MAP(CInDanNum, CDialogEx)
	ON_BN_CLICKED(IDOK, &CInDanNum::OnBnClickedOk)
END_MESSAGE_MAP()


// CInDanNum 消息处理程序


void CInDanNum::OnBnClickedOk()
{
	// TODO:  在此添加控件通知处理程序代码
	GetDlgItemText(IDC_DANNUMBER, DanNum);
	//ProjectorTestSystemDlg->SetIndex.OnBnClickedOk();
	CDialogEx::OnOK();
}


BOOL CInDanNum::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// TODO:  在此添加额外的初始化
	m_InDanNum.SetFocus();
	return FALSE;  // return TRUE unless you set the focus to a control
	// 异常:  OCX 属性页应返回 FALSE
}
