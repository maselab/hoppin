using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace hoppin.GameSystem
{
    /// <summary>
    /// コピーユーティリティ
    /// </summary>
    public static class CopyUtility
    {
        /// <summary>
        /// 指定されたインスタンスの深いコピーを生成し返します
        /// </summary>
        /// <typeparam name="T">コピーするインスタンスの型</typeparam>
        /// <param name="original">コピー元のインスタンス</param>
        /// <returns>指定されたインスタンスの深いコピー</returns>
        public static T DeepCopy<T>(T original)
        {
            // シリアル化した内容を保持するメモリーストリームを生成
            MemoryStream stream = new MemoryStream();
            try
            {
                // バイナリ形式でシリアライズするためのフォーマッターを生成
                BinaryFormatter formatter = new BinaryFormatter();
                // コピー元のインスタンスをシリアライズ
                formatter.Serialize(stream, original);
                // メモリーストリームの現在位置を先頭に設定
                stream.Position = 0L;
                // メモリーストリームの内容を逆シリアル化
                return (T)formatter.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
