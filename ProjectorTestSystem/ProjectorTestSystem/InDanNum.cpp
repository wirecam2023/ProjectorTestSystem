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
	CString InSelectFromSql;
	int TypeCount;
	_variant_t TypeVal, BodyVal, SingleBodyVal, MainVal;
	GetDlgItemText(IDC_DANNUMBER, DanNum);
	InSelectFromSql.Format(_T("select * from ProjectorInformation_EncodingRules where TypeName = '%s'"), DanNum);
	OperateDB.OpenRecordset(InSelectFromSql);
	TypeCount = OperateDB.GetRecordCount();
	if (TypeCount==0)
	{
		MessageBox(_T("订单号与前缀类型名不匹配，请重新输入订单号"));
		OperateDB.CloseRecordset();
		return;
	}
	else
	{
		TypeVal = OperateDB.m_pRecordset->GetCollect(_T("TypeName"));
		BodyVal = OperateDB.m_pRecordset->GetCollect(_T("Prefix_BodyCode"));
		SingleBodyVal = OperateDB.m_pRecordset->GetCollect(_T("Prefix_OpticalCode"));
		MainVal = OperateDB.m_pRecordset->GetCollect(_T("Prefix_MotherboardEncoding"));
		MyTypeName = CheckNull(TypeVal);
		MyPrefixBodyCode = CheckNull(BodyVal);
		MyPrefixOpticalCode = CheckNull(SingleBodyVal);
		MyPrefixMotherboardEncoding = CheckNull(MainVal);
		ProjectorTestSystemDlg->SetIndex.SetDlgItemText(IDC_INDEXTYPE, MyTypeName);
		ProjectorTestSystemDlg->SetIndex.SetDlgItemText(IDC_TOOLINDEX, MyPrefixBodyCode);
		ProjectorTestSystemDlg->SetIndex.SetDlgItemText(IDC_SINGLETOOLINDEX, MyPrefixOpticalCode);
		ProjectorTestSystemDlg->SetIndex.SetDlgItemText(IDC_MAININDEX, MyPrefixMotherboardEncoding);
	}
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



/*查NULL*/
CString CInDanNum::CheckNull(_variant_t Source)
{
	CString DestStr;
	if (Source.vt == VT_NULL)
	{
		DestStr = "";
		return DestStr;
	}
	else
	{
		DestStr = Source.bstrVal;
		return DestStr;
	}
}