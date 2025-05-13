using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FileForceDeleter
{
    public partial class forced_delete_file : Form
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool MoveFileEx(string lpExistingFileName, string? lpNewFileName, MoveFileFlags dwFlags);

        [Flags]
        enum MoveFileFlags
        {
            MOVEFILE_REPLACE_EXISTING = 0x00000001,
            MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004
        }

        private readonly List<string> selectedFiles = new();

        public forced_delete_file()
        {
            InitializeComponent();
        }

        private void btnBrowseFiles_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Select Files to Delete"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in ofd.FileNames)
                {
                    if (!selectedFiles.Contains(file))
                    {
                        selectedFiles.Add(file);
                        AddFileLabel(file);
                    }
                }
            }
        }

        private void AddFileLabel(string filePath)
        {
            var panel = new Panel { Width = flowLayoutPanelFiles.Width - 30, Height = 30 };
            var label = new Label { Text = Path.GetFileName(filePath), Width = 300, AutoEllipsis = true };
            var button = new Button { Text = "❌", Width = 30, Height = 25, Tag = filePath };
            button.Click += (s, e) =>
            {
                var path = (string)((Button)s!).Tag!;
                selectedFiles.Remove(path);
                flowLayoutPanelFiles.Controls.Remove(panel);
            };
            panel.Controls.Add(label);
            panel.Controls.Add(button);
            label.Left = 5;
            button.Left = 310;
            flowLayoutPanelFiles.Controls.Add(panel);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!selectedFiles.Any())
            {
                MessageBox.Show("No files selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var successfullyDeleted = new List<string>();
            var scheduledForReboot = new List<string>();
            var failed = new List<string>();

            foreach (var filePath in selectedFiles.ToList())
            {
                if (!File.Exists(filePath))
                {
                    failed.Add($"{filePath} (Not found)");
                    continue;
                }

                try
                {
                    var fileInfo = new FileInfo(filePath);
                    fileInfo.IsReadOnly = false;

                    File.Delete(filePath);
                    successfullyDeleted.Add(filePath);
                }
                catch (IOException)
                {
                    if (TryScheduleDelete(filePath))
                        scheduledForReboot.Add(filePath);
                    else
                        failed.Add($"{filePath} (IOException)");
                }
                catch (UnauthorizedAccessException)
                {
                    if (TryScheduleDelete(filePath))
                        scheduledForReboot.Add(filePath);
                    else
                        failed.Add($"{filePath} (Access Denied)");
                }
            }

            // Clean up UI and memory
            foreach (var file in successfullyDeleted.Concat(scheduledForReboot))
            {
                selectedFiles.Remove(file);
                RemoveFileFromUI(file);
            }

            ShowSummary(successfullyDeleted, scheduledForReboot, failed);
        }

        private bool TryScheduleDelete(string filePath)
        {
            bool scheduled = MoveFileEx(filePath, null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);

            return scheduled;
        }

        private void RemoveFileFromUI(string filePath)
        {
            foreach (Control panel in flowLayoutPanelFiles.Controls)
            {
                if (panel is not Panel) continue;

                var label = panel.Controls.OfType<Label>().FirstOrDefault();
                if (label == null) continue;

                if (filePath.EndsWith(label.Text, StringComparison.OrdinalIgnoreCase))
                {
                    flowLayoutPanelFiles.Controls.Remove(panel);
                    panel.Dispose();
                    break;
                }
            }
        }

        private void ShowSummary(List<string> deleted, List<string> scheduled, List<string> failed)
        {
            string summary = "";

            if (deleted.Any())
                summary += $"✅ Deleted:\n{string.Join("\n", deleted.Select(Path.GetFileName))}\n\n";

            if (scheduled.Any())
                summary += $"🔁 Scheduled for Deletion on Reboot:\n{string.Join("\n", scheduled.Select(Path.GetFileName))}\n\n";

            if (failed.Any())
                summary += $"❌ Failed:\n{string.Join("\n", failed)}\n";

            MessageBox.Show(summary.Trim(), "Deletion Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (scheduled.Any())
            {
                var result = MessageBox.Show("Some files were scheduled for deletion on reboot.\n\nDo you want to restart now?",
                    "Restart Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo("shutdown", "/r /t 0")
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true
                    });
                }
            }
        }

    }
}
