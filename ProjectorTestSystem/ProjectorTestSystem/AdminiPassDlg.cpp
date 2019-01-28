// AdminiPassDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ProjectorTestSystem.h"
#include "AdminiPassDlg.h"
#include "afxdialogex.h"


// CAdminiPassDlg 对话框

IMPLEMENT_DYNAMIC(CAdminiPassDlg, CDialogEx)

CAdminiPassDlg::CAdminiPassDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CAdminiPassDlg::IDD, pParent)
{

}

CAdminiPassDlg::~CAdminiPassDlg()
{
}

void CAdminiPassDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_ADPASSWORD, m_PassWord);
}


BEGIN_MESSAGE_MAP(CAdminiPassDlg, CDialogEx)
	ON_BN_CLICKED(IDOK, &CAdminiPassDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// CAdminiPassDlg 消息处理程序


void CAdminiPassDlg::OnBnClickedOk()
{
	// TODO:  在此添加控件通知处理程序代码
	GetDlgItemText(IDC_ADPASSWORD,PassWord);
	CDialogEx::OnOK();
}


BOOL CAdminiPassDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// TODO:  在此添加额外的初始化
	m_PassWord.SetFocus();
	return FALSE;  // return TRUE unless you set the focus to a control
	// 异常:  OCX 属性页应返回 FALSE
}
