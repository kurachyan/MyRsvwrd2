using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rsvwrd
{
    public class CS_Rsvwrd
    {
        #region 共有領域
        private String _wbuf;
        private Boolean _empty;
        public String Wbuf
        {
            get
            {
                return (_wbuf);
            }
            set
            {
                _wbuf = value;
                if (_wbuf == null)
                {   // 設定情報は無し？
                    _empty = true;
                }
                else
                {
                    _empty = false;
                }
            }
        }

        enum RsvCode
        {   // 予約語グループ
            RSV_NONE = 0,       // 未定義
            RSV_CLASS,          // 予約語１：クラス
            RSV_EXCEPT,         // 予約語２：例外
            RSV_ACCESS,         // 予約語３：アクセス修飾子
            RSV_CONTROL,        // 予約語４：制御フロー文
            RSV_DATA,           // 予約語５：データ型
            RSV_OTHER           // 予約語６：その他
        }
        private RsvCode _rsvcode;
        public Boolean Rsv
        {   // 予約語有無確認　[false]:予約語なし [true]:予約語あり
            get
            {
                if (_rsvcode == RsvCode.RSV_NONE)
                {   // 予約語未検出？
                    return (false);
                }
                else
                {   // 予約語検出
                    return (true);
                }
            }
        }
        public int RsvKind
        {
            get
            {
                return ((int)_rsvcode);
            }
        }

        // 予約語１：クラス
        private string[] _RsvTable1 =
        {   // [Class]
            "class",
            "interface",
            "struct",
            "abstract",
            "internal"
        };
        // 予約語２：例外
        private string[] _RsvTable2 =
        {   // [Exception]
            "catch",
            "finally",
            "throw",
            "try"
        };
        // 予約語３：アクセス修飾子
        private string[] _RsvTable3 =
        {   // [access modifier]
            "private",
            "protected",
            "public"
        };
        // 予約語４：制御フロー文
        private string[] _RsvTable4 =
        {   // [control flow statement]
            "if",
            "else",
            "switch",
            "case",
            "default",
            "while",
            "do",
            "for",
            "foreach",
            "break",
            "continue",
            "return",
            "goto"
        };
        // 予約語５：データ型
        private string[] _RsvTable5 =
        {   // [data type]
            "bool",
            "char",
            "byte",
            "short",
            "int",
            "long",
            "float",
            "double",
            "sbyte",
            "ushort",
            "uint",
            "ulong",
            "object",
            "string",
            "decimal"
        };
        // 予約語６：その他　　　
        private string[] _RsvTable6 =
        {   // [other type]
            "as",
            "base",
            "checked",
            "const",
            "delegate",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "fixed",
            "implicit",
            "in",
            "is",
            "lock",
            "namespace",
            "new",
            "null",
            "operator",
            "out",
            "override",
            "params",
            "readonly",
            "ref",
            "sealed",
            "sizeof",
            "stackalloc",
            "static",
            "this",
            "true",
            "typeof",
            "using",
            "unchecked",
            "unsafe",
            "virtual",
            "void",
            "volatile"
        };

        private char[] _trim = { ' ', '\t', '\r', '\n' };
        #endregion

        #region コンストラクタ
        public CS_Rsvwrd()
        {   // コンストラクタ
            _wbuf = null;       // 設定情報無し
            _empty = true;

            _rsvcode = RsvCode.RSV_NONE;    // 予約語：未定義
        }
        #endregion

        #region モジュール
        public void Clear()
        {   // 作業領域の初期化
            _wbuf = null;       // 設定情報無し
            _empty = true;

            _rsvcode = RsvCode.RSV_NONE;    // 予約語：未定義
        }
        public void Exec()
        {   // 予約語確認
            if (!_empty)
            {   // バッファーに実装有り
                _wbuf = _wbuf.Trim(_trim);       // 右側余白情報を削除

                if (_wbuf.Length == 0 || _wbuf == null)
                {   // バッファー情報無し
                    Clear();           // 作業領域の初期化
                }
                else
                {   // 予約語検索・評価
                    foreach (var __rsv in _RsvTable1)
                    {   // クラス関連の予約語を評価する
                        if (__rsv == _wbuf)
                        {   // 予約語定義が有るか？
                            _rsvcode = RsvCode.RSV_CLASS;    // 予約語：クラス
                            break;
                        }
                    }
                    if (_rsvcode == RsvCode.RSV_NONE)
                    {   // 予約語は未定義？
                        foreach (var __rsv in _RsvTable2)
                        {   // 例外関連の予約語を評価する
                            if (__rsv == _wbuf)
                            {   // 予約語定義が有るか？
                                _rsvcode = RsvCode.RSV_EXCEPT;    // 予約語：例外
                               break;
                            }
                        }
                    }
                    if (_rsvcode == RsvCode.RSV_NONE)
                    {   // 予約語は未定義？
                        foreach (var __rsv in _RsvTable3)
                        {   // アクセス修飾子関連の予約語を評価する
                            if (__rsv == _wbuf)
                            {   // 予約語定義が有るか？
                                _rsvcode = RsvCode.RSV_ACCESS;    // 予約語：アクセス修飾子
                                break;
                            }
                        }
                    }
                    if (_rsvcode == RsvCode.RSV_NONE)
                    {   // 予約語は未定義？
                        foreach (var __rsv in _RsvTable4)
                        {   // 制御フロー関連の予約語を評価する
                            if (__rsv == _wbuf)
                            {   // 予約語定義が有るか？
                                _rsvcode = RsvCode.RSV_CONTROL;    // 予約語：制御フロー
                                break;
                            }
                        }
                    }
                    if (_rsvcode == RsvCode.RSV_NONE)
                    {   // 予約語は未定義？
                        foreach (var __rsv in _RsvTable5)
                        {   // データ型関連の予約語を評価する
                            if (__rsv == _wbuf)
                            {   // 予約語定義が有るか？
                                _rsvcode = RsvCode.RSV_DATA;    // 予約語：データ型
                                break;
                            }
                        }
                    }
                    if (_rsvcode == RsvCode.RSV_NONE)
                    {   // 予約語は未定義？
                        foreach (var __rsv in _RsvTable6)
                        {   // その他関連の予約語を評価する
                            if (__rsv == _wbuf)
                            {   // 予約語定義が有るか？
                                _rsvcode = RsvCode.RSV_OTHER;    // 予約語：その他
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void Exec(String msg)
        {   // 予約語確認
            Setbuf(msg);                 // 入力内容の作業領域設定

            if (!_empty)
            {   // バッファーに実装有り
                _wbuf = _wbuf.Trim(_trim);       // 右側余白情報を削除

                if (_wbuf.Length == 0 || _wbuf == null)
                {   // バッファー情報無し
                    Clear();           // 作業領域の初期化
                }
                else
                {   // 予約語検索・評価
                    foreach (var __rsv in _RsvTable1)
                    {   // クラス関連の予約語を評価する
                        if (__rsv == _wbuf)
                        {   // 予約語定義が有るか？
                            _rsvcode = RsvCode.RSV_CLASS;    // 予約語：クラス
                            break;
                        }
                    }
                    if (_rsvcode == RsvCode.RSV_NONE)
                    {   // 予約語は未定義？
                        foreach (var __rsv in _RsvTable2)
                        {   // 例外関連の予約語を評価する
                            if (__rsv == _wbuf)
                            {   // 予約語定義が有るか？
                                _rsvcode = RsvCode.RSV_EXCEPT;    // 予約語：例外
                                break;
                            }
                        }
                    }
                    if (_rsvcode == RsvCode.RSV_NONE)
                    {   // 予約語は未定義？
                        foreach (var __rsv in _RsvTable3)
                        {   // アクセス修飾子関連の予約語を評価する
                            if (__rsv == _wbuf)
                            {   // 予約語定義が有るか？
                                _rsvcode = RsvCode.RSV_ACCESS;    // 予約語：アクセス修飾子
                                break;
                            }
                        }
                    }
                    if (_rsvcode == RsvCode.RSV_NONE)
                    {   // 予約語は未定義？
                        foreach (var __rsv in _RsvTable4)
                        {   // 制御フロー関連の予約語を評価する
                            if (__rsv == _wbuf)
                            {   // 予約語定義が有るか？
                                _rsvcode = RsvCode.RSV_CONTROL;    // 予約語：制御フロー
                                break;
                            }
                        }
                    }
                    if (_rsvcode == RsvCode.RSV_NONE)
                    {   // 予約語は未定義？
                        foreach (var __rsv in _RsvTable5)
                        {   // データ型関連の予約語を評価する
                            if (__rsv == _wbuf)
                            {   // 予約語定義が有るか？
                                _rsvcode = RsvCode.RSV_DATA;    // 予約語：データ型
                                break;
                            }
                        }
                    }
                    if (_rsvcode == RsvCode.RSV_NONE)
                    {   // 予約語は未定義？
                        foreach (var __rsv in _RsvTable6)
                        {   // その他関連の予約語を評価する
                            if (__rsv == _wbuf)
                            {   // 予約語定義が有るか？
                                _rsvcode = RsvCode.RSV_OTHER;    // 予約語：その他
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void Setbuf(String _strbuf)
        {   // [_wbuf]情報設定
            _wbuf = _strbuf;
            if (_wbuf == null)
            {   // 設定情報は無し？
                _empty = true;
            }
            else
            {
                _empty = false;
            }

            _rsvcode = RsvCode.RSV_NONE;    // 予約語：未定義
        }
        #endregion

    }
}
