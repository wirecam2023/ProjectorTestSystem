#include "stdafx.h"
#include "AdoDB.h"
// 默认构造函数
// 初始化数据库连接字符串
// 初始化OLE库
CAdo::CAdo(void)
{
	m_szErrorMsg = "";

	// 默认数据库连接字符串
	m_szConnectionString = " ";

	// 初始化OLE库
	CoInitialize(NULL);

	// 创建命令对象
	CreateCommand();

	// 创建连接对象
	CreateConnection();

	// 创建结果集对象
	CreateRecordset();
}

//
// 析构函数
//
CAdo::~CAdo(void)
{
	// 执行清理内存工作
	CloseConnection();
	CloseRecordset();

	if (m_pCmd)
	{
		m_pCmd.Release();
	}

	if (m_pRecordset)
	{
		m_pRecordset.Release();
	}

	if (m_pConn)
	{
		m_pConn.Release();
	}
}

/******************************************************************************
*函数名：   CreateConnection
*功能描述： 创建数据库连接对象
*前置条件： 数据库连接对象不存在
*后置条件： 创建完成一个数据库连接对象
*返回值：   TRUE--创建数据库连接对象成功
*           FALSE--创建数据库连接对象失败
*******************************************************************************/
bool CAdo::CreateConnection()
{
	// 如果创建失败，则返回FALSE
	if (FAILED(m_pConn.CreateInstance(__uuidof(Connection))))
	{
		return FALSE;
	}

	return TRUE;
}

/******************************************************************************
*函数名：   CreateCommand
*功能描述： 创建数据库命令对象
*前置条件： 数据库命令对象不存在
*后置条件： 创建完成一个数据库命令对象
*返回值：   TRUE--创建数据库命令对象成功
*           FALSE--创建数据库命令对象失败
*******************************************************************************/
bool CAdo::CreateCommand()
{
	if (FAILED(m_pCmd.CreateInstance(__uuidof(Command))))
	{
		return FALSE;
	}

	return TRUE;
}

/******************************************************************************
*函数名：   CreateRecordset
*功能描述： 创建结果集对象
*前置条件： 结果集对象不存在
*后置条件： 创建完成一个结果集对象
*返回值：   TRUE--创建结果集对象成功
*           FALSE--创建结果集对象失败
*******************************************************************************/
bool CAdo::CreateRecordset()
{
	if (FAILED(m_pRecordset.CreateInstance(__uuidof(Recordset))))
	{
		return FALSE;
	}

	return TRUE;
}

/******************************************************************************
*函数名：   OpenConnection
*功能描述： 打开数据库
*前置条件： 数据库连接对象不为空
*后置条件： 打开数据库以提供后续操作
*返回值：   TRUE--打开数据库成功
*           FALSE--打开数据库失败
*******************************************************************************/
bool CAdo::OpenConnection()
{
	try
	{
		// 关闭当前连接对象
		CloseConnection();

		// 用FAILED来测试，不能用!来测试
		if (FAILED(m_pConn->Open(_bstr_t(m_szConnectionString), _bstr_t(UserId), _bstr_t(Passwoord), adConnectUnspecified)))
		{
			return FALSE;
		}

		if (FAILED(CreateCommand()))
		{
			return FALSE;
		}

		m_pConn->CursorLocation = adUseClient;
		m_pCmd->ActiveConnection = m_pConn;

		return TRUE;
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();
		return FALSE;
	}
}


/******************************************************************************
*函数名：   CloseConnection
*功能描述： 关闭数据库
*前置条件： 数据库连接对象不为空
*后置条件： 关闭数据库以提供后续操作
*返回值：   TRUE--关闭数据库成功
*           FALSE--关闭数据库失败
*******************************************************************************/
bool CAdo::CloseConnection()
{
	try
	{
		// 关闭结果集
		CloseRecordset();

		// 关闭连接对象
		if (m_pConn != NULL && m_pConn->GetState() != adStateClosed)
		{
			m_pConn->Close();
		}

		return true;
	}
	catch (_com_error& e)
	{
		m_szErrorMsg = e.ErrorMessage();
		return false;
	}
}

/******************************************************************************
*函数名：   CloseRecordset
*功能描述： 关闭结果集对象
*前置条件： 结果集对象不为空
*后置条件： 关闭结果集对象以提供后续操作
*返回值：   TRUE--关闭结果集对象成功
*           FALSE--关闭结果集对象失败
*******************************************************************************/
bool CAdo::CloseRecordset()
{
	try
	{
		if (IsRecordsetOpened())
			m_pRecordset->Close();
		return true;
	}
	catch (_com_error& e)
	{
		m_szErrorMsg = e.ErrorMessage();
		return false;
	}
}

/******************************************************************************
*函数名：   IsConnected
*功能描述： 判断数据库连接是否已经连接
*前置条件： 数据库连接对象已经存在
*后置条件： 返回连接对象的连接状态真假值
*返回值：   TRUE--数据库连接对象已经处于连接状态
*           FALSE--数据库连接对象已经不处于连接状态
*******************************************************************************/
bool CAdo::IsConnected()
{
	if (m_pConn == NULL)
	{
		return FALSE;
	}

	if (m_pConn->GetState() == adStateClosed)
	{
		return FALSE;
	}

	return TRUE;
}

/******************************************************************************
*函数名：   IsRecordsetOpened
*功能描述： 判断数据库结果集对象是否已经打开
*前置条件： 数据库结果集对象已经存在
*后置条件： 返回结果集对象的连接状态真假值
*返回值：   TRUE--数据库结果集对象已经处于连接状态
*           FALSE--数据库结果集对象已经不处于连接状态
*******************************************************************************/
bool CAdo::IsRecordsetOpened()
{
	if (m_pRecordset == NULL)
	{
		return FALSE;
	}

	if (m_pRecordset->GetState() == adStateClosed)
	{
		return FALSE;
	}

	return TRUE;
}

/******************************************************************************
*函数名：   IsEndRecordset
*功能描述： 判断数据库结果集对象是否已经到达末尾
*前置条件： 数据库结果集对象已经存在
*后置条件： 返回结果集对象是否到达末尾真假值
*返回值：   TRUE--数据库结果集对象已经到达末尾
*           FALSE--数据库结果集对象还没有到达末尾
*******************************************************************************/
bool CAdo::IsEndRecordset()
{
	try
	{
		if (m_pRecordset->adoEOF == VARIANT_TRUE)
		{
			return TRUE;
		}
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();
		return FALSE;
	}

	return FALSE;
}

/******************************************************************************
*函数名：   MoveToNext
*功能描述： 移到到下一行
*前置条件： 数据库结果集有数据
*后置条件： 游标移到下一行
*******************************************************************************/
void CAdo::MoveToNext()
{
	try
	{
		if (!IsEndRecordset())
		{
			m_pRecordset->MoveNext();
		}
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();
	}
}

/******************************************************************************
*函数名：   MoveToPrevious
*功能描述： 移到到上一行
*前置条件： 数据库结果集有数据
*后置条件： 游标移到上一行
*******************************************************************************/
void CAdo::MoveToPrevious()
{
	try
	{
		// 只有结果集处于打开状态，且记录行数至少要有2行
		if (IsRecordsetOpened() && GetRecordCount() >= 2)
		{
			m_pRecordset->MovePrevious();
		}
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();
	}
}

/******************************************************************************
*函数名：   MoveToFirst
*功能描述： 游标移到首行
*前置条件： 数据库结果集有数据
*后置条件： 将游标移到首行
*******************************************************************************/
void CAdo::MoveToFirst()
{
	try
	{
		// 只有结果集处于打开状态，且记录行数至少要有一行
		if (IsRecordsetOpened() && GetRecordCount() >= 1)
		{
			m_pRecordset->MoveFirst();
		}
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();
	}
}

/******************************************************************************
*函数名：   MoveToLast
*功能描述： 游标移到最后一行
*前置条件： 数据库结果集有数据
*后置条件： 将游标移到结果集最后一行
*******************************************************************************/
void CAdo::MoveToLast()
{
	try
	{
		// 只有结果集处于打开状态，且记录行数至少要有一行
		if (IsRecordsetOpened() && GetRecordCount() >= 1)
		{
			m_pRecordset->MoveLast();
		}
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();
	}
}


/******************************************************************************
*函数名：   GetRecordCount()
*功能描述： 判断数据库结果录的条集中记数
*前置条件： 数据库结果集对象已经存在
*后置条件： 返回结果集记录的条数
*返回值：   long类型--记录的条数
*******************************************************************************/
long CAdo::GetRecordCount()
{
	try
	{
		if (m_pRecordset == NULL)
			return 0;

		// 返回记录行数
		return m_pRecordset->GetRecordCount();
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();            // 记录出错信息
	}

	return FALSE;
}

/******************************************************************************
*函数名：   OpenRecordset
*功能描述： 打开结果集对象
*前置条件： 数据库结果集对象不为空
*后置条件： 打开结果集对象以提供后续操作
*参数：     T-SQL语句
*返回值：   TRUE--打开结果集对象成功
*           FALSE--打开结果集对象失败
*******************************************************************************/
bool CAdo::OpenRecordset(CString szSQL)
{
	try
	{
		// 如果关闭时不出现异常
		if (OpenConnection() && CloseRecordset())
		{
			m_pRecordset->Open((_variant_t)szSQL,          // T-SQL语句
				m_pConn.GetInterfacePtr(), // 连接对象接口
				adOpenDynamic,             // 动态打开 
				adLockOptimistic,          // 锁定
				adCmdText);                // 操作类型为命令文本
		}
		return TRUE;
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();                  // 记录出错信息
		return FALSE;
	}
}

/******************************************************************************
*函数名：   ExecuteByConnection
*功能描述： 执行增、删、改操作
*前置条件： 数据库已经连接上
*后置条件： 返回受影响的行数
*返回值：   TRUE--操作成功
*           FALSE-操作失败
*参数：     T-SQL语句：szSQL：CString类型
*******************************************************************************/
bool CAdo::ExecuteByConnection(CString szSQL)
{
	try
	{
		// 如果打开失败，则退出
		if (!OpenConnection())
		{
			return FALSE;
		}

		// 受影响的行数
		_variant_t recordAffected;
		m_pRecordset= m_pConn->Execute(_bstr_t(szSQL), &recordAffected, adCmdText);
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();
		return FALSE;
	}

	return TRUE;
}


/******************************************************************************
*函数名：   GetErrorMsg
*功能描述： 获取出错的信息
*后置条件： 返回出错信息
*返回值：   出错的信息--类型：CString
*******************************************************************************/
CString CAdo::GetErrorMsg()
{
	return m_szErrorMsg;
}

/******************************************************************************
*函数名：   GetFieldValue
*功能描述： 获取字段值
*前置条件： 结果集有数据
*后置条件： 返回指定的字段的值
*返回值：   TRUE--获取到字段的值
*           FALSE-获取不到字段的值
*参数：     pFieldName : cahr *类型--标识字段名
*           strValue: CString类型--标识指定的字段的值
*******************************************************************************/
bool CAdo::GetFieldValue(char * pFieldName, CString& strValue)
{
	try
	{
		if (!IsRecordsetOpened())
			return FALSE;

		// 获取字段值
		_variant_t vtFieldValue = m_pRecordset->Fields->GetItem(pFieldName)->Value;

		if (vtFieldValue.vt == VT_BSTR)  // 如果是字符串类型
		{
			strValue = (char *)(_bstr_t)vtFieldValue;
			strValue.Trim();
			return true;
		}
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();
	}

	return FALSE;
}

/******************************************************************************
*函数名：   IsFieldValueHasExisted
*功能描述： 判断指定的字段值是否已在存在
*返回值：   TRUE--指定的字段的值已经存在
*           FALSE-指定的字段的值不存在
*参数：     pFieldName : cahr *类型--标识字段名
*           strValue: CString类型--标识指定的字段的值
*******************************************************************************/
bool CAdo::IsFieldValueHasExisted(char * pFieldName, CString& strValue)
{
	try
	{
		CString strTempValue;

		// 获取字段值
		if (GetFieldValue(pFieldName, strTempValue))
		{
			if (strTempValue.Trim() == strValue)
			{
				return TRUE;
			}
		}
	}
	catch (_com_error &e)
	{
		m_szErrorMsg = e.ErrorMessage();
	}

	return FALSE;
}
