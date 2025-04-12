using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms.VisualStyles;

namespace RemoteDaoGuanHelper;
    
public class SecureOverlay : Form
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        using var configForm = new ConfigForm();

        var result = configForm.ShowDialog();
        
        if (result == DialogResult.OK)
        {
            // 使用配置的文本创建主窗口
            Application.Run(new SecureOverlay(configForm.LabelText));
        }
        else
        {
            MessageBox.Show(@"已取消导管");
        }
    }
    
    private SecureOverlay(string labelText)
    {
        InitializeForm(labelText);
        Load += (_, _) => ApplySecuritySettings();
    }
        
    private void InitializeForm(string labelText)
    {
        // 基础窗体设置
        FormBorderStyle = FormBorderStyle.None;
        WindowState = FormWindowState.Maximized;
        TopMost = true;
        BackColor = Color.Black;

        // 创建演示内容
        var label = new Label
        {
            Text = labelText,
            ForeColor = Color.White,
            Font = new Font(string.Empty, 64, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(100, 100),
        };
        Controls.Add(label);
    }
        
    private void ApplySecuritySettings()
    {
        SetWindowDisplayAffinity(Handle, WdaExcludeFromCapture);

        // 设置窗口扩展样式
        var currentExStyle = GetWindowLong(Handle, GwlExStyle);
        SetWindowLong(Handle, GwlExStyle, currentExStyle | WsExLayered | WsExTransparent | WsExTopmost);
    }
        
    [DllImport("user32.dll")]
    private static extern bool SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);
        
    // 常量定义
    private const uint WdaExcludeFromCapture = 0x00000011;
    private const int WsExTransparent = 0x00000020;
    private const int WsExLayered = 0x00080000;
    private const int WsExTopmost = 0x00000008;

    // 其他Windows API函数
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private const int GwlExStyle = -20;
}

public class ConfigForm : Form
{
    public string LabelText { get; private set; } = string.Empty; // 默认文本
    
    private TextBox _textBox;
    private Button _confirmButton;
    private Button _cancelButton;

    public ConfigForm()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        // 窗口设置
        Text = @"配置遮罩文本";
        Size = new Size(260, 150);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        MaximizeBox = false;
        MinimizeBox = false;

        // 文本框
        _textBox = new TextBox
        {
            Location = new Point(20, 20),
            Size = new Size(200, 30),
            Text = @"远程导管中，勿扰🤫",
        };

        // 确认按钮
        _confirmButton = new Button
        {
            Text = @"开始远程导管",
            DialogResult = DialogResult.OK,
            Location = new Point(20, 60),
            Size = new Size(100, 30),
        };
        _confirmButton.Click += (_, _) => Confirm();

        // 取消按钮
        _cancelButton = new Button
        {
            Text = @"取消远程导管",
            DialogResult = DialogResult.Cancel,
            Location = new Point(120, 60),
            Size = new Size(100, 30),
        };

        // 添加控件
        Controls.Add(_textBox);
        Controls.Add(_confirmButton);
        Controls.Add(_cancelButton);

        // 设置确认按钮回车键响应
        AcceptButton = _confirmButton;
    }

    private void Confirm()
    {
        if (!string.IsNullOrWhiteSpace(_textBox.Text))
        {
            LabelText = _textBox.Text.Trim();
        }
    }
}