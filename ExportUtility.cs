/// <summary>
/// export objs as Excel file
/// </summary>
/// <param name="token">usualy is User.Name</param>
/// <returns></returns>
public static Stream ToExcel<T>(T[] objs, ReportConfigurationModel reportConfiguration, IDictionary<string, Func<T, string>> columnRules = null, string[] ignoreColumns = null, string token = "") where T : class
{
    /// 
    bool hasCallbackToken = !string.IsNullOrEmpty(token);
    if (hasCallbackToken)
    {
        if (!_progress.Keys.Contains(token))
        {
            _progress.Add(token, 0);
        }
        else
        {
            _progress[token] = 0;
        }
    }
    int step = objs.Length / 100 == 0 ? 1 : objs.Length / 100, lastTraceRowIdx = headerRowIdx, rowIdx = headerRowIdx;
    /// ... 
    foreach (T obj in objs)
    {
        /// ...
        /// some heavy-time-cost code here
        rowIdx++;
        if (hasCallbackToken && (rowIdx - lastTraceRowIdx) >= step)
        {
            _progress[token] = (rowIdx - headerRowIdx) * 100 / objs.Length;
            lastTraceRowIdx = rowIdx;
        }
    }
    _progress[token] = 100;
    // return memoryStream;
}

private static IDictionary<string, int> _progress = new Dictionary<string, int>();
/// <summary>
/// to get latest status of exporting
/// </summary>
/// <param name="token">usualy is User.Name</param>
/// <returns></returns>
public static int GetExportingProgress(string token)
{
    const int min = 0, max = 100;
    if (_progress.Keys.Contains(token) )
    {
        int p = _progress[token];
        if(p.Equals(max))
        {
            // reset status
            _progress.Remove(token);
        }
        return p;
    }
    return min;
}
