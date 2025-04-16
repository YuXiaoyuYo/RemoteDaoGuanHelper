namespace RemoteDaoGuanHelper.Forms;

public class ConfigForm : Form
{
    public string LabelText { get; private set; } = string.Empty; // 默认文本

    private TextBox _textBox;
    private Button _confirmButton;

    public ConfigForm()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        // 窗口设置
        Text = @"配置远程导管方案";
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
            Location = new Point(70, 60),
            Size = new Size(100, 30),
        };
        _confirmButton.Click += (_, _) => Confirm();

        // 添加控件
        Controls.Add(_textBox);
        Controls.Add(_confirmButton);

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
