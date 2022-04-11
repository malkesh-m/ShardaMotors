using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShardaMotorsWeb_App.App_Data.DAL
{
    public class DALBase
    {
        #region VARS
        protected SqlConnection _dataConnection;
        protected SqlCommand _dataCommand;

        protected string _connectString;

        private ArrayList Parms = new ArrayList();
        //private int _currentUserID = -1;

        //public int _userID;
        // private Logger _logger = Logger.getInstance();
        public readonly System.Data.SqlTypes.SqlDateTime DateTimeNull = System.Data.SqlTypes.SqlDateTime.Null;
        #endregion

        #region CONSTRUCTORS
        public DALBase()
        {
            try
            {
                _connectString = GetConnectionStr();  //ConfigHelper.Get("ConnectionString");

                _dataConnection = new SqlConnection(_connectString);    // Create a connection to the database
                _dataCommand = _dataConnection.CreateCommand();         // Create sql Command and attach to connection

                //_dataCommand.CommandTimeout = 0;

            }
            catch (Exception ex)
            {
                //throw ex;
              //  ExceptionLogging.SendErrorToText(ex, "DALBase", "BitdojiLibrary.Dal.DALBase");
            }
        }

        public static string GetConnectionStr()
        {
            //return ConfigurationManager.ConnectionStrings["PropertyControldb"].ToString();
            return ConfigurationSettings.AppSettings.Get("PropertyControldb");

        }

        #endregion

        #region TRANSACTIONS
        //public void CreateCommittableTransaction()
        //{
        //    _cTxn = new CommittableTransaction();
        //}

        //public void Rollback()
        //{
        //    if (_cTxn != null)
        //    {
        //        _cTxn.Rollback();
        //        _cTxn = null;
        //    }
        //    else
        //        throw new Exception("No transaction open");
        //}

        //public void Commit()
        //{
        //    if (_cTxn != null)
        //    {
        //        _cTxn.Commit();
        //        _cTxn = null;
        //    }
        //    else
        //        throw new Exception("No transaction open");
        //}


        public void TransactionStart()

        // Starts a new transaction block
        {
            //TransactionScope txnScope = new TransactionScope();
        }


        public void TransactionComplete()
        {
            //txnScope.Complete();
            //txnScope.Dispose();
        }


        public void TransactionRollback()

        // Called from a catch block to release the transaction with out commit.
        {
            //txnScope.Dispose();
        }
        #endregion

        #region PARAMETERS

        protected void AddParameter(string ParmName, SqlDbType A_DbType, object oValue)
        {
            if (oValue == null)
                oValue = DBNull.Value;


            SqlParameter oPm = new SqlParameter(ParmName, A_DbType);
            oPm.Value = oValue;

            Parms.Add(oPm);
        }

        /// <summary>
        /// Add output parameter to list.
        /// </summary>
        /// <param name="ParmName"></param>
        /// <param name="A_dbtype"></param>
        /// <param name="value"></param>
        protected void AddOutputParameter(string ParmName, SqlDbType A_dbtype, object oValue)
        {
            if (oValue == null)
                oValue = DBNull.Value;

            SqlParameter oPm = new SqlParameter(ParmName, A_dbtype);
            oPm.Value = oValue;
            oPm.Direction = ParameterDirection.Output;

            Parms.Add(oPm);
        }


        /// <summary>
        /// Allows for direction parameter
        /// </summary>
        /// <param name="ParmName"></param>
        /// <param name="A_DbType"></param>
        /// <param name="oValue"></param>
        /// <param name="pd"></param>
        protected void AddParameter(string ParmName, SqlDbType A_DbType, object oValue, ParameterDirection pd, int size)
        {
            if (oValue == null)
                oValue = DBNull.Value;

            SqlParameter oPm = new SqlParameter(ParmName, A_DbType);
            oPm.Value = oValue;
            oPm.Direction = ParameterDirection.Input;
            oPm.Direction = pd;
            oPm.Size = size;
            Parms.Add(oPm);
        }
        #endregion

        #region DATA_ACCESS
        public DataSet ExecuteForData(string sStoredProcedureName)
        {
            DataSet ds = new DataSet();
            try
            {
                //                using (TransactionScope txnScope = new TransactionScope())
                //                {
                //Open Connection
                _dataConnection.Open();
                //Call Stored Procedure
                _dataCommand.CommandText = sStoredProcedureName;
                _dataCommand.CommandType = CommandType.StoredProcedure;

                // Add in the parameters
                for (int i = 0; i < Parms.Count; i++)
                    _dataCommand.Parameters.Add((SqlParameter)Parms[i]);

                SqlDataAdapter adapter = new SqlDataAdapter(_dataCommand);
                adapter.Fill(ds);

                // string z = ds.GetXml();
                //                    txnScope.Complete();
                //                }
            }
            catch (Exception ex)
            {
                ds = null;
                throw ex;
                // ExceptionLogging.SendErrorToText(ex, "ExecuteForData", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {
                //close the connection
                _dataConnection.Close();


                _dataCommand.Parameters.Clear();            // Remove saved parms
                Parms.Clear();
            }

            return ds;
        }

        /// <summary>
        /// This method is marked as protected because the dataconnection is not closed inside of this method.
        /// The implementing DAL classes are the only one with access and they MUST close the dataconnection
        /// when finished with the datareader.
        /// </summary>
        /// <param name="StoredProcedureName"></param>
        /// <returns>DataReader with an open connection to the database.</returns>
        protected SqlDataReader ExecuteForDataReader(string StoredProcedureName)
        {
            SqlDataReader sdr = null;

            try
            {
                _dataConnection.Open();
                _dataCommand.CommandText = StoredProcedureName;
                _dataCommand.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < Parms.Count; i++)
                {
                    _dataCommand.Parameters.Add((SqlParameter)Parms[i]);
                }

                sdr = _dataCommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
                //ExceptionLogging.SendErrorToText(ex, "ExecuteForDataReader", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {
                ClearParms();
            }

            //return new SqlDataReader(sdr);
            return sdr;
        }

        protected SqlDataReader ExecuteDataReader(string StoredProcedureName)
        {
            SqlDataReader sdr = null;

            try
            {
                _dataConnection.Open();

                _dataCommand.CommandText = StoredProcedureName;
                _dataCommand.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < Parms.Count; i++)
                {
                    _dataCommand.Parameters.Add((SqlParameter)Parms[i]);
                }

                sdr = _dataCommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
                // ExceptionLogging.SendErrorToText(ex, "ExecuteDataReader", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {
                ClearParms();
            }

            return sdr;
        }

        /// <summary>
        /// Close database connection
        /// </summary>
        public void CloseConnection()
        {
            if (_dataConnection != null)
                _dataConnection.Close();
        }

        /// <summary>
        /// </summary>
        /// <param name="sStoredProcedureName"></param>
        /// <returns></returns>
        internal object ExecuteScalar(string sStoredProcedureName)
        {
            //  Output Parameter should be last parameter entered.
            //  Returns Integer type.
            object oRetVal = 0;

            try
            {
                //              using (TransactionScope txnScope = new TransactionScope())
                //                {
                //Open Connection
                _dataConnection.Open();

                //Call Stored Procedure
                _dataCommand.CommandText = sStoredProcedureName;
                _dataCommand.CommandType = CommandType.StoredProcedure;

                // Add in the parameters
                for (int i = 0; i < Parms.Count; i++)
                {
                    _dataCommand.Parameters.Add((SqlParameter)Parms[i]);
                }

                oRetVal = _dataCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
                // ExceptionLogging.SendErrorToText(ex, "ExecuteScalar", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {
                //close the connection
                _dataConnection.Close();

                ClearParms();
            }

            return oRetVal;
        }

        /// <summary>
        /// Using Output Parameters Assumes the last parameter given is the output parameter
        /// and returns that as a
        /// </summary>
        /// <param name="sStoredProcedureName"></param>
        /// <returns></returns>
        internal object ExecuteScalarOutput(string sStoredProcedureName)
        {
            //  Output Parameter should be last parameter entered.
            //  Returns Integer type.
            object oRetVal = 0;

            try
            {
                //              using (TransactionScope txnScope = new TransactionScope())
                //                {
                //Open Connection
                _dataConnection.Open();

                //Call Stored Procedure
                _dataCommand.CommandText = sStoredProcedureName;
                _dataCommand.CommandType = CommandType.StoredProcedure;

                // Add in the parameters
                for (int i = 0; i < Parms.Count; i++)
                {
                    _dataCommand.Parameters.Add((SqlParameter)Parms[i]);
                }

                _dataCommand.ExecuteNonQuery();

                // Take last parameter value and assume  that is the output parameter
                oRetVal = _dataCommand.Parameters[_dataCommand.Parameters.Count - 1].Value;

            }
            catch (Exception ex)
            {
                throw ex;
                //  ExceptionLogging.SendErrorToText(ex, "ExecuteScalarOutput", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {
                //close the connection
                _dataConnection.Close();

                ClearParms();
            }

            return oRetVal;
        }

        /// <summary>
        /// For use with ExecuteScalarOutput, to clear the parameters after execution of the sproc.
        /// </summary>
        public void ClearParms()
        {
            _dataCommand.Parameters.Clear();
            Parms.Clear();
        }

        internal object ExecuteNonQueryWithParm(string sStoredProcedureName)
        {
            // Output Parameter should be last parameter entered.
            // Returns Integer type.
            //int iParmResponse = -1;
            object oRetVal = 0;

            try
            {
                //              using (TransactionScope txnScope = new TransactionScope())
                //                {
                //Open Connection
                _dataConnection.Open();

                //Call Stored Procedure
                _dataCommand.CommandText = sStoredProcedureName;
                _dataCommand.CommandType = CommandType.StoredProcedure;

                // Add in the parameters
                for (int i = 0; i < Parms.Count; i++)
                {
                    _dataCommand.Parameters.Add((SqlParameter)Parms[i]);
                }

                //_dataCommand.ExecuteNonQuery();
                //oRetVal  = new object();
                oRetVal = _dataCommand.ExecuteScalar();

                //                    txnScope.Complete();
                //                }

            }
            catch (Exception ex)
            {
                throw ex;
                //  ExceptionLogging.SendErrorToText(ex, "ExecuteNonQueryWithParm", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {
                //close the connection
                _dataConnection.Close();

                ClearParms();
            }

            return oRetVal;
        }

        /// <summary>
        /// Execute a dynamically prepared sql string.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteDynamicSQL(string sql)
        {
            try
            {
                _dataConnection.Open();

                _dataCommand.CommandText = sql;
                _dataCommand.CommandType = CommandType.Text;

                _dataCommand.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw ex;
                // ExceptionLogging.SendErrorToText(ex, "ExecuteDynamicSQL", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {
                // close the connection
                _dataConnection.Close();

                ClearParms();
            }
            return true;
        }

        internal bool ExecuteNonQuery(string sStoredProcedureName)
        {
            bool bSuccess = false;

            try
            {
                //                using (TransactionScope txnScope = new TransactionScope())
                //                {
                //Open Connection
                _dataConnection.Open();

                //Call Stored Procedure
                _dataCommand.CommandText = sStoredProcedureName;
                _dataCommand.CommandType = CommandType.StoredProcedure;

                // Add in the parameters
                for (int i = 0; i < Parms.Count; i++)
                {
                    _dataCommand.Parameters.Add((SqlParameter)Parms[i]);
                }

                _dataCommand.ExecuteNonQuery();

                bSuccess = true;
                //                    txnScope.Complete();
                //                }

            }
            catch (Exception ex)
            {
                bSuccess = false;
                throw ex;
                // ExceptionLogging.SendErrorToText(ex, "ExecuteNonQuery", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {
                //close the connection
                _dataConnection.Close();

                ClearParms();
            }

            return bSuccess;
        }

        /// <summary>
        /// Returns a Data Table from query
        /// </summary>
        /// <param name="sproc"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sproc)
        {
            DataTable dt = new DataTable();

            try
            {

                _dataConnection.Open();
                //Call Stored Procedure
                _dataCommand.CommandText = sproc;
                _dataCommand.CommandType = CommandType.StoredProcedure;

                // Add in the parameters
                for (int i = 0; i < Parms.Count; i++)
                    _dataCommand.Parameters.Add((SqlParameter)Parms[i]);

                SqlDataAdapter adapter = new SqlDataAdapter(_dataCommand);
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;

            }
            finally
            {
                //close the connection
                _dataConnection.Close();


                _dataCommand.Parameters.Clear();            // Remove saved parms
                Parms.Clear();
            }

            return dt;
        }
        #endregion

        #region BULK_INSERTS
        //public void BulkTableInsert(DataSet objDS, string tablename)
        //{
        //    try
        //    {
        //        //Change the column mapping first.
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder(1000);
        //        System.IO.StringWriter sw = new System.IO.StringWriter(sb);
        //        foreach (DataColumn col in objDS.Tables[tablename].Columns)
        //        {
        //            col.ColumnMapping = System.Data.MappingType.Element;
        //        }
        //        objDS.WriteXml(sw, System.Data.XmlWriteMode.WriteSchema);
        //        string sqlText = buildBulkUpdateSql(sb.ToString(), objDS.Tables[tablename]);
        //        ExecuteDynamicSQL(sqlText);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //private string buildBulkUpdateSql(string dataXml, DataTable table)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    dataXml = dataXml.Replace(Environment.NewLine, "");
        //    dataXml = dataXml.Replace("\"", "''");
        //    //init the xml doc
        //    sb.Append(" SET NOCOUNT ON");
        //    sb.Append(" DECLARE @hDoc INT");
        //    sb.AppendFormat(" EXEC sp_xml_preparedocument @hDoc OUTPUT, '{0}'", dataXml);

        //    //This code inserts new data.
        //    sb.AppendFormat(" INSERT INTO {0} SELECT *", table.TableName);
        //    sb.AppendFormat(" FROM OPENXML (@hdoc, '/NewDataSet/{0}', 2) WITH {0}", table.TableName);
        //    //clear the xml doc
        //    sb.Append(" EXEC sp_xml_removedocument @hDoc");
        //    return sb.ToString();
        //}
        #endregion

        #region DATAREADER

        public sealed class SmartDataReader
        {
            #region DATAREADER_VARS
            private DateTime defaultDate;
            SqlDataReader _reader;
            //  Logger _lg = Logger.getInstance();
            #endregion

            #region DATAREADER_CONSTRUCTOR
            public SmartDataReader(SqlDataReader reader)
            {
                this.defaultDate = DateTime.MinValue;
                _reader = reader;
            }
            #endregion

            #region DATA_ACCESSORS
            public int GetInt32(String column)
            {
                try
                {
                    return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                            ? (int)0 : (int)_reader[column];

                }
                catch (Exception ex)
                {
                    throw ex;
                    //ExceptionLogging.SendErrorToText(ex, "GetInt32", "BitdojiLibrary.Dal.DALBase");
                }

                return 0;
            }

            public short GetInt16(String column)
            {
                try
                {
                    return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                           ? (short)0 : (short)_reader[column];
                }
                catch //(Exception ex)
                {
#if DEBUG2
                     _lg.WriteLog(ex.Message, Logger.MsgType.Error);
#endif
                }
                return 0;
            }

            public float GetFloat(String column)
            {
                try
                {
                    return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                ? 0 : float.Parse(_reader[column].ToString());
                }
                catch //(Exception ex)
                {
#if DEBUG2
                   _lg.WriteLog(ex.Message, Logger.MsgType.Error);
#endif
                }
                return 0;
            }

            public double GetDouble(String column)
            {
                try
                {
                    return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                ? 0 : double.Parse(_reader[column].ToString());
                }
                catch //(Exception ex)
                {
#if DEBUG2
                    _lg.WriteLog(ex.Message, Logger.MsgType.Error);
#endif
                }
                return 0;
            }

            public decimal GetDecimal(String column)
            {
                try
                {
                    return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                ? 0 : decimal.Parse(_reader[column].ToString());
                }
                catch //(Exception ex)
                {
#if DEBUG2
                    _lg.WriteLog(ex.Message, Logger.MsgType.Error);
#endif
                }
                return 0;
            }

            public bool GetBoolean(String column)
            {
                try
                {
                    return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                              ? false : (bool)_reader[column];
                }
                catch //(Exception ex)
                {

                    try
                    {
                        return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                             ? false : Convert.ToBoolean(_reader[column]);
                    }
                    catch //(Exception ex)
                    {
#if DEBUG2
                  // _lg.WriteLog(ex.Message, Logger.MsgType.Error);
#endif
                    }

                }
                return false;
            }

            public String GetString(String column)
            {
                try
                {
                    return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                       ? null : _reader[column].ToString();
                }
                catch //(Exception ex)
                {
#if DEBUG2
                   _lg.WriteLog(ex.Message, Logger.MsgType.Error);
#endif
                }
                return null;
            }

            public DateTime GetDateTime(String column)
            {
                try
                {
                    return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                   ? defaultDate : (DateTime)_reader[column];
                }
                catch //(Exception ex)
                {
#if DEBUG2
                    _lg.WriteLog(ex.Message, Logger.MsgType.Error);
#endif
                }
                return DateTime.Parse("1/1/1900");
            }

            public Decimal GetMoney(String column)
            {
                try
                {
                    //decimal fl;        // = _reader.IsDBNull(_reader.GetOrdinal(column)) ? 0 : (decimal) _reader[column];
                    if (!_reader.IsDBNull(_reader.GetOrdinal(column)))
                        return Convert.ToDecimal(_reader[column]);

                    //float fl = (float) _reader.GetOrdinal(column);   //_reader[column];  //_reader.GetOrdinal(column);
                    return (decimal)0;

                    //return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                    //               ? 0 : (Decimal)_reader[column];
                }
                catch  //(Exception ex)
                {
#if DEBUG2
                   _lg.WriteLog(ex.Message, Logger.MsgType.Error);
#endif
                }
                return 0;
            }

            //public  GetMoney(String column)
            //{
            //    Money data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
            //                       ? defaultDate : (DateTime)_reader[column];
            //    return data;
            //}
            public byte[] GetBytes(String column)
            {
                try
                {
                    return (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                ? null : ((byte[])_reader[column]);
                }
                catch //(Exception ex)
                {
#if DEBUG2
                    _lg.WriteLog(ex.Message, Logger.MsgType.Error);
#endif
                }
                return null;
            }

            #endregion

            #region DATAREADER_ACCESS
            public bool Read()
            {
                return _reader.Read();
            }

            public bool HasRows()
            {
                return _reader.HasRows;
            }

            public bool NextResult()
            {
                return _reader.NextResult();
            }

            public void Close()
            {
                _reader.Close();
                _reader.Dispose();
            }
            #endregion
        }

        #endregion

        #region STATIC METHODS
        /// <summary>
        /// Returns Dataset, which can contain multiple datatables.   Typically we use just get Datatable but if needed this is works also.
        /// </summary>
        /// <param name="sproc"></param>
        /// <returns></returns>
        public static DataSet sGetDataSet(string sproc)
        {
            SqlConnection DataConnection = null;
            SqlCommand DataCommand = null;
            SqlDataAdapter Adapter = null;
            SqlDataReader Reader = null;
            DataSet results = new DataSet();

            try
            {
                DataConnection = new SqlConnection(GetConnectionStr());     // Gets the Data Conn String 1st and then the actual db connection obj

                DataCommand = DataConnection.CreateCommand();

                DataConnection.Open();
                DataCommand.CommandText = sproc;
                DataCommand.CommandType = CommandType.StoredProcedure;

                Adapter = new SqlDataAdapter(DataCommand);
                Adapter.Fill(results);
            }
            catch (Exception ex)
            {
                throw ex;
                // ExceptionLogging.SendErrorToText(ex, "sGetDataSet", "BitdojiLibrary.Dal.DALBase");

            }
            finally
            {
                try
                {
                    Reader.Close();
                    Reader.Dispose();
                }
                catch { }

                try
                {
                    DataCommand.Dispose();
                }
                catch { }

                try
                {
                    DataConnection.Close();
                    DataConnection.Dispose();
                }
                catch { }
            }

            return results;
        }

        /// <summary>
        /// Returns a DataTable back to the caller.  Encapsulates all of the normal ADO plumbing.
        /// </summary>
        /// <param name="DB"></param>
        /// <param name="sproc"></param>
        /// <returns></returns>
        public static DataTable sGetDataTable(string sproc)
        {
            SqlConnection DataConnection = null;
            SqlCommand DataCommand = null;
            SqlDataAdapter Adapter = null;
            SqlDataReader Reader = null;
            DataTable results = new DataTable();

            try
            {
                DataConnection = new SqlConnection(GetConnectionStr());     // Gets the Data Conn String 1st and then the actual db connection obj

                DataCommand = DataConnection.CreateCommand();

                DataConnection.Open();
                DataCommand.CommandText = sproc;
                DataCommand.CommandType = CommandType.StoredProcedure;

                Adapter = new SqlDataAdapter(DataCommand);
                Adapter.Fill(results);
            }
            catch (Exception ex)
            {
                throw ex;
                // ExceptionLogging.SendErrorToText(ex, "sGetDataTable", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {
                try
                {
                    Reader.Close();
                    Reader.Dispose();
                }
                catch { }

                try
                {
                    DataCommand.Dispose();
                }
                catch { }

                try
                {
                    DataConnection.Close();
                    DataConnection.Dispose();
                }
                catch { }
            }

            return results;
        }

        /// <summary>
        /// Execute a Dynamic SQL statement which returns a dataTable
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public static DataTable sGetDataTableDynamic(string SQL)
        {
            SqlConnection DataConnection = null;
            SqlCommand DataCommand = null;
            SqlDataAdapter Adapter = null;
            SqlDataReader Reader = null;
            DataTable results = new DataTable();

            try
            {
                DataConnection = new SqlConnection(GetConnectionStr());
                DataCommand = DataConnection.CreateCommand();
                DataCommand.CommandTimeout = 3600;
                DataConnection.Open();
                DataCommand.CommandText = SQL;
                DataCommand.CommandType = CommandType.Text;

                Adapter = new SqlDataAdapter(DataCommand);
                Adapter.Fill(results);
            }
            catch (Exception ex)
            {
                throw ex;
                //ExceptionLogging.SendErrorToText(ex, "sGetDataTableDynamic", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {
                try
                {
                    Reader.Close();
                    Reader.Dispose();
                }
                catch { }

                try
                {
                    DataCommand.Dispose();
                }
                catch { }

                try
                {
                    DataConnection.Close();
                    DataConnection.Dispose();
                }
                catch { }
            }

            return results;
        }


        /// <summary>
        /// Execute a Dynamic SQL Non-Query (Insert, Update, Delete)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public static void sExectuteNonQueryDynamic(string SQL)
        {
            SqlConnection DataConnection = null;
            SqlCommand dcmd = null;

            try
            {
                DataConnection = new SqlConnection(GetConnectionStr());

                dcmd = DataConnection.CreateCommand();

                DataConnection.Open();
                dcmd.CommandText = SQL;
                dcmd.CommandType = CommandType.Text;

                dcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
                // ExceptionLogging.SendErrorToText(ex, "sExectuteNonQueryDynamic", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {

                try
                {
                    dcmd.Dispose();
                }
                catch { }

                try
                {
                    DataConnection.Close();
                    DataConnection.Dispose();
                }
                catch { }
            }

        }

        /// <summary>
        /// Execute a Dynamic SQL Non-Query (Insert, Update, Delete)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public static object sExecuteScalarDynamic(string SQL)
        {
            SqlConnection DataConnection = null;
            SqlCommand dcmd = null;

            try
            {
                DataConnection = new SqlConnection(GetConnectionStr());

                dcmd = DataConnection.CreateCommand();

                DataConnection.Open();
                dcmd.CommandText = SQL;
                dcmd.CommandType = CommandType.Text;


            }
            catch (Exception ex)
            {
                throw ex;
                // ExceptionLogging.SendErrorToText(ex, "sExecuteScalarDynamic", "BitdojiLibrary.Dal.DALBase");
            }
            finally
            {

                try
                {
                    dcmd.Dispose();
                }
                catch { }

                try
                {
                    DataConnection.Close();
                    DataConnection.Dispose();
                }
                catch { }
            }
            return dcmd.ExecuteScalar();
        }


        #endregion

        #region METHOD
        private static DataTable CreateDataTable<T>(IList<T> obj)
        {
            DataTable table = new DataTable();
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();
            foreach (var prop in propertyInfos)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var id in obj)
            {
                table.Rows.Add(id);
            }
            return table;
        }
        #endregion

    }
}
