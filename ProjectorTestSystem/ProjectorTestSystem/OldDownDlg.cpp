// OldDownDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ProjectorTestSystem.h"
#include "OldDownDlg.h"
#include "afxdialogex.h"


// COldDownDlg 对话框


/*全局变量*/
int OldDownFirstRow = 0;

IMPLEMENT_DYNAMIC(COldDownDlg, CDialogEx)

COldDownDlg::COldDownDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(COldDownDlg::IDD, pParent)
	, m_OldDownStaticVal(_T(""))
	, m_OldDownEditVal(_T(""))
{

}

COldDownDlg::~COldDownDlg()
{
}

void COldDownDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST1, m_OldDownlist);
	DDX_Control(pDX, IDC_OLDDOWN_STATIC, m_OldDown);
	DDX_Text(pDX, IDC_OLDDOWN_STATIC, m_OldDownStaticVal);
	//DDX_Control(pDX, IDC_EDIT3, m_OldDownEdit);
	//DDX_Text(pDX, IDC_EDIT3, m_OldDownEditVal);
	DDX_Text(pDX, IDC_OLDDOWN, m_OldDownEditVal);
	DDX_Control(pDX, IDC_OLDDOWN, m_OldDownEdit);
}


BEGIN_MESSAGE_MAP(COldDownDlg, CDialogEx)
END_MESSAGE_MAP()


// COldDownDlg 消息处理程序


BOOL COldDownDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// TODO:  在此添加额外的初始化
	m_OldDownlist.SetExtendedStyle(m_OldDownlist.GetExtendedStyle() | LVS_EX_FULLROWSELECT | LVS_EX_GRIDLINES);
	m_OldDownlist.InsertColumn(0, _T("机身码"), LVCFMT_CENTER, 150, 0);
	m_OldDownlist.InsertColumn(1, _T("第一次测试时间"), LVCFMT_CENTER, 150, 1);
	return TRUE;  // return TRUE unless you set the focus to a control
	// 异常:  OCX 属性页应返回 FALSE
}



/*回车键响应*/
BOOL COldDownDlg::PreTranslateMessage(MSG* pMsg)
{
	// TODO:  在此添加专用代码和/或调用基类
	CString m_OldDownEditStr, OldDownSelectSql, OldDownTimeStr, OldDownUpdataSql;
	_variant_t OldUpTime;
	int m_OldDownStaticLength;
	LONG OldDownRecordestCount;
	UpdateData(TRUE);
	if (pMsg->message == WM_KEYDOWN && pMsg->wParam == VK_RETURN)
	{
		if (GetFocus()->GetDlgCtrlID() == IDC_OLDDOWN)
		{
			m_OldDownStaticLength = m_OldDownStaticVal.GetLength();
			m_OldDownEditStr = m_OldDownEditVal.Left(m_OldDownStaticLength);
			if (m_OldDownEditStr != m_OldDownStaticVal || m_OldDownEditVal == "")
			{
				MessageBox(_T("机身码错误"));
				m_OldDownEdit.SetFocus();
				m_OldDownEditVal = "";
				UpdateData(FALSE);
				return CDialogEx::PreTranslateMessage(pMsg);
			}
			OldDownSelectSql.Format(_T("select * from ProjectorInformation_MainTable where FuselageCode = '%s'"), m_OldDownEditVal);
			OperateDB.OpenRecordset(OldDownSelectSql);
			OldDownRecordestCount = OperateDB.GetRecordCount();
			if (OldDownRecordestCount == 0)
			{
				MessageBox(_T("不存在的机身码"));
				OperateDB.CloseRecordset();
				m_OldDownEdit.SetFocus();
				m_OldDownEditVal = "";
				UpdateData(FALSE);
				return CDialogEx::PreTranslateMessage(pMsg);
			}
			if (!OperateDB.m_pRecordset->BOF)
				OperateDB.m_pRecordset->MoveFirst();
			OldUpTime = OperateDB.m_pRecordset->GetCollect(_T("AgeingBeginTime"));
			if (OldUpTime.vt==VT_NULL)
			{
				MessageBox(_T("该产品没有上架老化"));
				OperateDB.CloseRecordset();
				m_OldDownEdit.SetFocus();
				m_OldDownEditVal = "";
				UpdateData(FALSE);
				return CDialogEx::PreTranslateMessage(pMsg);
			}
			else
			{
				OldDownTimeStr = GetTime();
				OldDownUpdataSql.Format(_T("UPDATE ProjectorInformation_MainTable SET AgeingEndTime = '%s' WHERE FuselageCode = '%s'"), OldDownTimeStr, m_OldDownEditVal);
				OperateDB.ExecuteByConnection(OldDownUpdataSql);
				m_OldDownlist.InsertItem(OldDownFirstRow, m_OldDownEditVal);
				m_OldDownlist.SetItemText(OldDownFirstRow, 1, OldDownTimeStr);
				OperateDB.CloseRecordset();
				m_OldDownEdit.SetFocus();
				m_OldDownEditVal = "";
				UpdateData(FALSE);
			}

		}
		OldDownFirstRow++;
	}
	return CDialogEx::PreTranslateMessage(pMsg);
}

/*获取当前时间*/
CString COldDownDlg::GetTime()
{
	CTime time = CTime::GetCurrentTime();
	CString Tiemstr;
	Tiemstr = time.Format(_T("%Y-%m-%d  %H:%M:%S"));
	return Tiemstr;
}



void COldDownDlg::OnOK()
{
	// TODO:  在此添加专用代码和/或调用基类

	/*CDialogEx::OnOK();*/
}


void COldDownDlg::OnCancel()
{
	// TODO:  在此添加专用代码和/或调用基类

	/*CDialogEx::OnCancel();*/
}
