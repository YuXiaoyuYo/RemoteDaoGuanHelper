namespace RemoteDaoGuanHelper.Forms;

public class SecureForm : Form
{
    public SecureForm(string labelText)
    {
        InitializeForm(labelText);
        Load += (_, _) => Services.WindowService.ApplySecuritySettings(this);
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

    
}

