﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace AutoMouse2
{

  public partial class Form1 : Form
  {
    /* Mouse & Keybord Detection */
    [DllImport("USER32.dll")]
      public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

    /* 使うキーの個数用意する */
    /* click - F1 - F2 - F3 - ... - F12 - Q */
    private bool[] flgClick = {false, false, false, false, false, false, false, false, false, false, false, false, false, false};
    private int wait_millisec = 0;
    /* 記録される操作の数 */
    private int record_cnt = 0;
    private int run_count = 0;
    private bool Record_flag = false;

    /* 記録される操作の種類(と操作された座標) */
    List<int> cmd_Record = new List<int>();
    List<int> cmd_Record_x = new List<int>();
    List<int> cmd_Record_y = new List<int>();
    /* 次の操作までの待機時間 */
    List<int> wait_time_Record = new List<int>();
    private bool run_flag = false;

    /* 待機時間の計測に使用 */
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

    /* keyboard control */
    [DllImport("user32.dll")]
      static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    /* Mouse control */
    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
      static extern void SetCursorPos(int X, int Y);

    [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
      static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    private const int MOUSEEVENTF_MOVED = 0x0001;
    private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const int MOUSEEVENTF_LEFTUP = 0x0004;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
    private const int MOUSEEVENTF_RIGHTUP = 0x0010;
    private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
    private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
    private const int MOUSEEVENTF_WHEEL = 0x0080;
    private const int MOUSEEVENTF_XDOWN = 0x0100;
    private const int MOUSEEVENTF_XUP = 0x0200;
    private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

    private const int SCREEN_LENGTH = 0x10000;



    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      timer1.Start();
    }

    /* Hookと記録の実行 */
    private void timer1_Tick(object sender, EventArgs e)
    {
      // 記録した操作実行時の処理
      // Qキーが押されたら終了
      if (run_flag && flgClick[13] == false)
      {

        if (GetAsyncKeyState(Keys.Q) != 0)
        {
          if (flgClick[13] == false)
          {
            run_flag = false;
            flgClick[13] = true;
            run_count = 0;
          }
        }
        else
        {
          flgClick[13] = false;
        }

        // 記録した操作がループするように
        if (run_count > record_cnt-2)
        {
          run_count = 0;
        }
        switch (cmd_Record[run_count])
        {
          case 0:
            SetCursorPos(cmd_Record_x[run_count], cmd_Record_y[run_count]);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);  // マウスの左ボタンダウンイベントを発生させる
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);    // マウスの左ボタンアップイベントを発生させる
            break;

          case 1:
            // F1キーの押し下げをシミュレートする。
            keybd_event((byte)Keys.F1, 0, 0, (UIntPtr)0);
            // F1キーの解放をシミュレートする。
            keybd_event((byte)Keys.F1, 0, 2, (UIntPtr)0);
            break;

          case 2:
            keybd_event((byte)Keys.F2, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F2, 0, 2, (UIntPtr)0);
            break;

          case 3:
            keybd_event((byte)Keys.F3, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F3, 0, 2, (UIntPtr)0);
            break;

          case 4:
            keybd_event((byte)Keys.F4, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F4, 0, 2, (UIntPtr)0);
            break;

          case 5:
            keybd_event((byte)Keys.F5, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F5, 0, 2, (UIntPtr)0);
            break;

          case 6:
            keybd_event((byte)Keys.F6, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F6, 0, 2, (UIntPtr)0);
            break;

          case 7:
            keybd_event((byte)Keys.F7, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F7, 0, 2, (UIntPtr)0);
            break;

          case 8:
            keybd_event((byte)Keys.F8, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F8, 0, 2, (UIntPtr)0);
            break;

          case 9:
            keybd_event((byte)Keys.F9, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F9, 0, 2, (UIntPtr)0);
            break;

          case 10:
            keybd_event((byte)Keys.F10, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F10, 0, 2, (UIntPtr)0);
            break;

          case 11:
            keybd_event((byte)Keys.F11, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F11, 0, 2, (UIntPtr)0);
            break;

          case 12:
            keybd_event((byte)Keys.F12, 0, 0, (UIntPtr)0);
            keybd_event((byte)Keys.F12, 0, 2, (UIntPtr)0);
            break;
        }
        
        // 次の操作までの待機
        sw.Restart();
        while (true)
        {
          TimeSpan ts = sw.Elapsed;
          if (ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds >= wait_time_Record[run_count])
          {
            break;
          }
          
          // Qキーが押されたら終了
          if (GetAsyncKeyState(Keys.Q) != 0)
          {
            if (flgClick[13] == false)
            {
              run_flag = false;
              //label1.Text = "F1キーが押されました";
              flgClick[13] = true;
              run_count = 0;
            }
          }
          else
          {
            flgClick[13] = false;
          }

        }
        run_count++;
      }

      if (GetAsyncKeyState(Keys.Q) != 0)
      {
        if (flgClick[13] == false)
        {
          run_flag = false;
          //label1.Text = "F1キーが押されました";
          flgClick[13] = true;
          run_count = 0;
        }
      }
      else
      {
        flgClick[13] = false;
      }

      // 操作記録時の処理
      if (Record_flag)
      {
        // 左クリックを記録
        if (GetAsyncKeyState(Keys.LButton) != 0)
        {
          if (flgClick[0] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(0);
            cmd_Record_x.Add(Cursor.Position.X);
            cmd_Record_y.Add(Cursor.Position.Y);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();

            flgClick[0] = true;
          }
        }
        else
        {
          flgClick[0] = false;
        }

        // F1キーを記録
        if (GetAsyncKeyState(Keys.F1) != 0)
        {
          if (flgClick[1] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(1);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[1] = true;
          }
        }
        else
        {
          flgClick[1] = false;
        }

        if (GetAsyncKeyState(Keys.F2) != 0)
        {
          if (flgClick[2] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(2);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[2] = true;
          }
        }
        else
        {
          flgClick[2] = false;
        }

        if (GetAsyncKeyState(Keys.F3) != 0)
        {
          if (flgClick[3] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(3);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[3] = true;
          }
        }
        else
        {
          flgClick[3] = false;
        }

        if (GetAsyncKeyState(Keys.F4) != 0)
        {
          if (flgClick[4] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(4);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[4] = true;
          }
        }
        else
        {
          flgClick[4] = false;
        }

        if (GetAsyncKeyState(Keys.F5) != 0)
        {
          if (flgClick[5] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(5);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[5] = true;
          }
        }
        else
        {
          flgClick[5] = false;
        }

        if (GetAsyncKeyState(Keys.F6) != 0)
        {
          if (flgClick[6] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(6);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[6] = true;
          }
        }
        else
        {
          flgClick[6] = false;
        }

        if (GetAsyncKeyState(Keys.F7) != 0)
        {
          if (flgClick[7] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(7);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[7] = true;
          }
        }
        else
        {
          flgClick[7] = false;
        }

        if (GetAsyncKeyState(Keys.F8) != 0)
        {
          if (flgClick[8] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(8);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[8] = true;
          }
        }
        else
        {
          flgClick[8] = false;
        }

        if (GetAsyncKeyState(Keys.F9) != 0)
        {
          if (flgClick[9] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(9);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[9] = true;
          }
        }
        else
        {
          flgClick[9] = false;
        }

        if (GetAsyncKeyState(Keys.F10) != 0)
        {
          if (flgClick[10] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(10);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[10] = true;
          }
        }
        else
        {
          flgClick[10] = false;
        }

        if (GetAsyncKeyState(Keys.F11) != 0)
        {
          if (flgClick[11] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(11);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[11] = true;
          }
        }
        else
        {
          flgClick[11] = false;
        }

        if (GetAsyncKeyState(Keys.F12) != 0)
        {
          if (flgClick[12] == false)
          {
            TimeSpan ts = sw.Elapsed;
            wait_millisec = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;
            cmd_Record.Add(12);
            cmd_Record_x.Add(-1);
            cmd_Record_y.Add(-1);
            wait_time_Record.Add(wait_millisec);
            record_cnt++;
            sw.Restart();
            flgClick[12] = true;
          }
        }
        else
        {
          flgClick[12] = false;
        }

      }
    }

    /* 操作記録 */
    private void button1_Click(object sender, EventArgs e)
    {
      if (Record_flag == false)
      {
        // 3秒待機
        for(int i = 3; i > 0; i--)
        {
          sw.Restart();
          while (true)
          {
            label1.Text = i.ToString();
            TimeSpan ts = sw.Elapsed;
            if(ts.Seconds >= 1 && ts.Milliseconds >= 0)
            {
              break;
            }

          }
        }
        label1.Text = "記録開始";
        Record_flag = true;
        sw.Restart();
      }
    }

    /* 記録終了 */
    private void button2_Click(object sender, EventArgs e)
    {
      if(Record_flag)
      {
        sw.Stop();
        label1.Text = "記録終了";
      }
      Record_flag = false;
    }

    /* 記録の実行開始 */
    private void button3_Click(object sender, EventArgs e)
    {
      if(run_flag == false && record_cnt > 1)
      {
        run_flag = true;
        label1.Text = "記録を実行";
        label2.Text = "Qで実行終了";
      }
      else
      {
        label1.Text = "操作が記録されていません";
      }
    }

    /* 記録の削除 */
    private void button4_Click(object sender, EventArgs e)
    {
      cmd_Record.Clear();
      cmd_Record_x.Clear();
      cmd_Record_y.Clear();
      wait_time_Record.Clear();
      record_cnt = 0;
    }
  }
}
