using System;
using System.Linq;
using System.Text;

namespace Kogane.DebugMenu
{
	/// <summary>
	/// システム情報を表示するクラス
	/// </summary>
	public sealed class UnityEngineInfoCreator : ListCreatorBase<ActionData>
	{
		//==============================================================================
		// 変数
		//==============================================================================
		private ActionData[] m_list;

		//==============================================================================
		// プロパティ
		//==============================================================================
		public override int Count => m_list.Length;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// リストの表示に使用するデータを作成します
		/// </summary>
		protected override void DoCreate( ListCreateData data )
		{
			m_list = ToText()
					.Split( '\n' )
					.Where( x => data.IsMatch( x ) )
					.Select( x => new ActionData( x ) )
					.ToArray()
				;

			if ( data.IsReverse )
			{
				Array.Reverse( m_list );
			}
		}

		/// <summary>
		/// 指定されたインデックスの要素の表示に使用するデータを返します
		/// </summary>
		protected override ActionData DoGetElemData( int index )
		{
			return m_list.ElementAtOrDefault( index );
		}

		/// <summary>
		/// テキストを整形して返します
		/// </summary>
		private static string ToText()
		{
#if ENABLE_IL2CPP
            const string IL2CPP = "True";
#else
			const string IL2CPP = "False";
#endif

			var builder = new StringBuilder();

			builder.AppendLine( "<b>Application</b>" );
			builder.AppendLine( "" );
			builder.AppendLine( JsonUnityEngineApplication.Get() );
			builder.AppendLine( "" );
			builder.AppendLine( "<b>Debug</b>" );
			builder.AppendLine( "" );
			builder.AppendLine( JsonUnityEngineDebug.Get() );
			builder.AppendLine( "" );
			builder.AppendLine( "<b>Scene Manager</b>" );
			builder.AppendLine( "" );
			builder.AppendLine( JsonUnityEngineSceneManagement.Get() );
			builder.AppendLine( "" );
			builder.AppendLine( "<b>Screen</b>" );
			builder.AppendLine( "" );
			builder.AppendLine( JsonUnityEngineScreen.Get() );
			builder.AppendLine( "" );
			builder.AppendLine( "<b>Time</b>" );
			builder.AppendLine( "" );
			builder.AppendLine( JsonUnityEngineTime.Get() );
			builder.AppendLine( "" );
			builder.AppendLine( "<b>System Info</b>" );
			builder.AppendLine( "" );
			builder.AppendLine( JsonUnityEngineSystemInfo.Get() );
			builder.AppendLine( "" );
			builder.AppendLine( "<b>Other</b>" );
			builder.AppendLine( "" );
			builder.AppendLine( $"IL2CPP: {IL2CPP}" );

			return builder.ToString();
		}
	}
}