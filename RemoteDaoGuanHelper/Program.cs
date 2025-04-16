using RemoteDaoGuanHelper.Forms;
using System.Reflection;

[assembly: AssemblyVersion("1.1.*")]

namespace RemoteDaoGuanHelper;

public abstract class Program
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

        // 使用配置的文本创建主窗口
        if (result == DialogResult.OK)
            Application.Run(new SecureForm(configForm.LabelText));
    }
}
