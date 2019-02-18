#pragma once
#include "afxwin.h"
#include "Resource.h"
// CDeleteSelectTip 对话框

class CDeleteSelectTip : public CDialogEx
{
	DECLARE_DYNAMIC(CDeleteSelectTip)

public:
	CDeleteSelectTip(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CDeleteSelectTip();

// 对话框数据
	enum { IDD = IDD_DELETESELECTTIP };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
};
