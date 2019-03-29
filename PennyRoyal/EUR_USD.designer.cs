﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PennyRoyal
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="EUR_USD")]
	public partial class EUR_USDDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSimpleTrendFollowing(SimpleTrendFollowing instance);
    partial void UpdateSimpleTrendFollowing(SimpleTrendFollowing instance);
    partial void DeleteSimpleTrendFollowing(SimpleTrendFollowing instance);
    partial void InsertBarData(BarData instance);
    partial void UpdateBarData(BarData instance);
    partial void DeleteBarData(BarData instance);
    #endregion
		
		public EUR_USDDataContext() : 
				base(global::PennyRoyal.Properties.Settings.Default.EUR_USDConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public EUR_USDDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EUR_USDDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EUR_USDDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EUR_USDDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SimpleTrendFollowing> SimpleTrendFollowings
		{
			get
			{
				return this.GetTable<SimpleTrendFollowing>();
			}
		}
		
		public System.Data.Linq.Table<BarData> BarDatas
		{
			get
			{
				return this.GetTable<BarData>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SimpleTrendFollowing")]
	public partial class SimpleTrendFollowing : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private System.Nullable<System.DateTime> _BarTime;
		
		private System.Nullable<System.DateTime> _SystemTime;
		
		private string _TimeFrame;
		
		private System.Nullable<decimal> _SMA50;
		
		private string _Trend;
		
		private System.Nullable<decimal> _StandardDeviation;
		
		private System.Nullable<decimal> _Distance;
		
		private System.Nullable<decimal> _TakeProfit;
		
		private System.Nullable<decimal> _StopLoss;
		
		private string _Trade;
		
		private System.Nullable<int> _ProfitTrades;
		
		private System.Nullable<int> _LossTrades;
		
		private System.Nullable<int> _TotalTrades;
		
		private System.Nullable<decimal> _AccountBalance;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnBarTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnBarTimeChanged();
    partial void OnSystemTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnSystemTimeChanged();
    partial void OnTimeFrameChanging(string value);
    partial void OnTimeFrameChanged();
    partial void OnSMA50Changing(System.Nullable<decimal> value);
    partial void OnSMA50Changed();
    partial void OnTrendChanging(string value);
    partial void OnTrendChanged();
    partial void OnStandardDeviationChanging(System.Nullable<decimal> value);
    partial void OnStandardDeviationChanged();
    partial void OnDistanceChanging(System.Nullable<decimal> value);
    partial void OnDistanceChanged();
    partial void OnTakeProfitChanging(System.Nullable<decimal> value);
    partial void OnTakeProfitChanged();
    partial void OnStopLossChanging(System.Nullable<decimal> value);
    partial void OnStopLossChanged();
    partial void OnTradeChanging(string value);
    partial void OnTradeChanged();
    partial void OnProfitTradesChanging(System.Nullable<int> value);
    partial void OnProfitTradesChanged();
    partial void OnLossTradesChanging(System.Nullable<int> value);
    partial void OnLossTradesChanged();
    partial void OnTotalTradesChanging(System.Nullable<int> value);
    partial void OnTotalTradesChanged();
    partial void OnAccountBalanceChanging(System.Nullable<decimal> value);
    partial void OnAccountBalanceChanged();
    #endregion
		
		public SimpleTrendFollowing()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BarTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> BarTime
		{
			get
			{
				return this._BarTime;
			}
			set
			{
				if ((this._BarTime != value))
				{
					this.OnBarTimeChanging(value);
					this.SendPropertyChanging();
					this._BarTime = value;
					this.SendPropertyChanged("BarTime");
					this.OnBarTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SystemTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> SystemTime
		{
			get
			{
				return this._SystemTime;
			}
			set
			{
				if ((this._SystemTime != value))
				{
					this.OnSystemTimeChanging(value);
					this.SendPropertyChanging();
					this._SystemTime = value;
					this.SendPropertyChanged("SystemTime");
					this.OnSystemTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TimeFrame", DbType="Text", UpdateCheck=UpdateCheck.Never)]
		public string TimeFrame
		{
			get
			{
				return this._TimeFrame;
			}
			set
			{
				if ((this._TimeFrame != value))
				{
					this.OnTimeFrameChanging(value);
					this.SendPropertyChanging();
					this._TimeFrame = value;
					this.SendPropertyChanged("TimeFrame");
					this.OnTimeFrameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SMA50", DbType="Decimal(18,9)")]
		public System.Nullable<decimal> SMA50
		{
			get
			{
				return this._SMA50;
			}
			set
			{
				if ((this._SMA50 != value))
				{
					this.OnSMA50Changing(value);
					this.SendPropertyChanging();
					this._SMA50 = value;
					this.SendPropertyChanged("SMA50");
					this.OnSMA50Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Trend", DbType="Text", UpdateCheck=UpdateCheck.Never)]
		public string Trend
		{
			get
			{
				return this._Trend;
			}
			set
			{
				if ((this._Trend != value))
				{
					this.OnTrendChanging(value);
					this.SendPropertyChanging();
					this._Trend = value;
					this.SendPropertyChanged("Trend");
					this.OnTrendChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StandardDeviation", DbType="Decimal(18,9)")]
		public System.Nullable<decimal> StandardDeviation
		{
			get
			{
				return this._StandardDeviation;
			}
			set
			{
				if ((this._StandardDeviation != value))
				{
					this.OnStandardDeviationChanging(value);
					this.SendPropertyChanging();
					this._StandardDeviation = value;
					this.SendPropertyChanged("StandardDeviation");
					this.OnStandardDeviationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Distance", DbType="Decimal(18,9)")]
		public System.Nullable<decimal> Distance
		{
			get
			{
				return this._Distance;
			}
			set
			{
				if ((this._Distance != value))
				{
					this.OnDistanceChanging(value);
					this.SendPropertyChanging();
					this._Distance = value;
					this.SendPropertyChanged("Distance");
					this.OnDistanceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TakeProfit", DbType="Decimal(18,9)")]
		public System.Nullable<decimal> TakeProfit
		{
			get
			{
				return this._TakeProfit;
			}
			set
			{
				if ((this._TakeProfit != value))
				{
					this.OnTakeProfitChanging(value);
					this.SendPropertyChanging();
					this._TakeProfit = value;
					this.SendPropertyChanged("TakeProfit");
					this.OnTakeProfitChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StopLoss", DbType="Decimal(18,9)")]
		public System.Nullable<decimal> StopLoss
		{
			get
			{
				return this._StopLoss;
			}
			set
			{
				if ((this._StopLoss != value))
				{
					this.OnStopLossChanging(value);
					this.SendPropertyChanging();
					this._StopLoss = value;
					this.SendPropertyChanged("StopLoss");
					this.OnStopLossChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Trade", DbType="Text", UpdateCheck=UpdateCheck.Never)]
		public string Trade
		{
			get
			{
				return this._Trade;
			}
			set
			{
				if ((this._Trade != value))
				{
					this.OnTradeChanging(value);
					this.SendPropertyChanging();
					this._Trade = value;
					this.SendPropertyChanged("Trade");
					this.OnTradeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProfitTrades", DbType="Int")]
		public System.Nullable<int> ProfitTrades
		{
			get
			{
				return this._ProfitTrades;
			}
			set
			{
				if ((this._ProfitTrades != value))
				{
					this.OnProfitTradesChanging(value);
					this.SendPropertyChanging();
					this._ProfitTrades = value;
					this.SendPropertyChanged("ProfitTrades");
					this.OnProfitTradesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LossTrades", DbType="Int")]
		public System.Nullable<int> LossTrades
		{
			get
			{
				return this._LossTrades;
			}
			set
			{
				if ((this._LossTrades != value))
				{
					this.OnLossTradesChanging(value);
					this.SendPropertyChanging();
					this._LossTrades = value;
					this.SendPropertyChanged("LossTrades");
					this.OnLossTradesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TotalTrades", DbType="Int")]
		public System.Nullable<int> TotalTrades
		{
			get
			{
				return this._TotalTrades;
			}
			set
			{
				if ((this._TotalTrades != value))
				{
					this.OnTotalTradesChanging(value);
					this.SendPropertyChanging();
					this._TotalTrades = value;
					this.SendPropertyChanged("TotalTrades");
					this.OnTotalTradesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccountBalance", DbType="Decimal(18,9)")]
		public System.Nullable<decimal> AccountBalance
		{
			get
			{
				return this._AccountBalance;
			}
			set
			{
				if ((this._AccountBalance != value))
				{
					this.OnAccountBalanceChanging(value);
					this.SendPropertyChanging();
					this._AccountBalance = value;
					this.SendPropertyChanged("AccountBalance");
					this.OnAccountBalanceChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.BarData")]
	public partial class BarData : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.DateTime _SystemTime;
		
		private System.DateTime _BarTime;
		
		private decimal _o;
		
		private decimal _h;
		
		private decimal _l;
		
		private decimal _c;
		
		private int _Volume;
		
		private int _ID;
		
		private string _Timeframe;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSystemTimeChanging(System.DateTime value);
    partial void OnSystemTimeChanged();
    partial void OnBarTimeChanging(System.DateTime value);
    partial void OnBarTimeChanged();
    partial void OnoChanging(decimal value);
    partial void OnoChanged();
    partial void OnhChanging(decimal value);
    partial void OnhChanged();
    partial void OnlChanging(decimal value);
    partial void OnlChanged();
    partial void OncChanging(decimal value);
    partial void OncChanged();
    partial void OnVolumeChanging(int value);
    partial void OnVolumeChanged();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnTimeframeChanging(string value);
    partial void OnTimeframeChanged();
    #endregion
		
		public BarData()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SystemTime", DbType="DateTime NOT NULL")]
		public System.DateTime SystemTime
		{
			get
			{
				return this._SystemTime;
			}
			set
			{
				if ((this._SystemTime != value))
				{
					this.OnSystemTimeChanging(value);
					this.SendPropertyChanging();
					this._SystemTime = value;
					this.SendPropertyChanged("SystemTime");
					this.OnSystemTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BarTime", DbType="DateTime NOT NULL")]
		public System.DateTime BarTime
		{
			get
			{
				return this._BarTime;
			}
			set
			{
				if ((this._BarTime != value))
				{
					this.OnBarTimeChanging(value);
					this.SendPropertyChanging();
					this._BarTime = value;
					this.SendPropertyChanged("BarTime");
					this.OnBarTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_o", DbType="Decimal(18,9) NOT NULL")]
		public decimal o
		{
			get
			{
				return this._o;
			}
			set
			{
				if ((this._o != value))
				{
					this.OnoChanging(value);
					this.SendPropertyChanging();
					this._o = value;
					this.SendPropertyChanged("o");
					this.OnoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_h", DbType="Decimal(18,9) NOT NULL")]
		public decimal h
		{
			get
			{
				return this._h;
			}
			set
			{
				if ((this._h != value))
				{
					this.OnhChanging(value);
					this.SendPropertyChanging();
					this._h = value;
					this.SendPropertyChanged("h");
					this.OnhChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_l", DbType="Decimal(18,9) NOT NULL")]
		public decimal l
		{
			get
			{
				return this._l;
			}
			set
			{
				if ((this._l != value))
				{
					this.OnlChanging(value);
					this.SendPropertyChanging();
					this._l = value;
					this.SendPropertyChanged("l");
					this.OnlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_c", DbType="Decimal(18,9) NOT NULL")]
		public decimal c
		{
			get
			{
				return this._c;
			}
			set
			{
				if ((this._c != value))
				{
					this.OncChanging(value);
					this.SendPropertyChanging();
					this._c = value;
					this.SendPropertyChanged("c");
					this.OncChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Volume", DbType="Int NOT NULL")]
		public int Volume
		{
			get
			{
				return this._Volume;
			}
			set
			{
				if ((this._Volume != value))
				{
					this.OnVolumeChanging(value);
					this.SendPropertyChanging();
					this._Volume = value;
					this.SendPropertyChanged("Volume");
					this.OnVolumeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Timeframe", DbType="VarChar(50)")]
		public string Timeframe
		{
			get
			{
				return this._Timeframe;
			}
			set
			{
				if ((this._Timeframe != value))
				{
					this.OnTimeframeChanging(value);
					this.SendPropertyChanging();
					this._Timeframe = value;
					this.SendPropertyChanged("Timeframe");
					this.OnTimeframeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
