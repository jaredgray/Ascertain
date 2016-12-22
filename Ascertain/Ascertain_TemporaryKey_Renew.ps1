Rename-Item "C:\SourceControl\PDX Windows\Ascertain\Ascertain\Ascertain_TemporaryKey.pfx" Ascertain_TemporaryKey.old.pfx


$ps = new-object System.Diagnostics.Process
$ps.StartInfo.Filename = "C:\SourceControl\PDX Windows\Ascertain\Ascertain\RenewCert.exe"
$ps.StartInfo.Arguments = "Ascertain_TemporaryKey.old.pfx Ascertain_TemporaryKey.pfx CN=ME2\jared"
$ps.StartInfo.RedirectStandardOutput = $True
$ps.StartInfo.UseShellExecute = $false
$ps.start()
$ps.WaitForExit()
[string] $Out = $ps.StandardOutput.ReadToEnd();

$Out

Rename-Item "C:\SourceControl\PDX Windows\Ascertain\Ascertain\Ascertain_TemporaryKey.old.pfx" Ascertain_TemporaryKey.pfx