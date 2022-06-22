using System.Text;

public class BranchDetails
{
  private string _workItemDetails = "";

  public string WorkItemDetails
  {
    get => _workItemDetails;
    set
    {
      _workItemDetails = value;
      BranchName = BuildBranchName(value);
    }
  }

  public string BranchName { get; set; } = "";

  public bool HasBranchName => BranchName != "";

  private string BuildBranchName(string? workItemDetails)
  {
    if (string.IsNullOrWhiteSpace(workItemDetails))
    {
      return "";
    }

    // Task 317520: add package yaml build
    // Product Backlog Item 311830: WAT Deployment Tool
    // Bug 301475: TCFS - ST - New exchange part with Original part usage and core part from DMS catalog, empty

    var sb = new StringBuilder("");

    var tail = "";

    if (workItemDetails.StartsWith("Bug"))
    {
      sb.Append("bugfix/");
      tail = workItemDetails.Substring("Bug".Length);
    }

    if (workItemDetails.StartsWith("Product Backlog Item"))
    {
      sb.Append("feature/");
      tail = workItemDetails.Substring("Product Backlog Item".Length);
    }

    if (workItemDetails.StartsWith("Task"))
    {
      sb.Append("feature/");
      tail = workItemDetails.Substring("Task".Length);
    }

    if (tail == "")
    {
      return "";
    }

    tail = tail.Trim();
    var isPreviousDash = false;

    foreach (char c in tail)
    {
      if (char.IsLetterOrDigit(c))
      {
        sb.Append(char.ToLowerInvariant(c));
        isPreviousDash = false;
      }
      else
      {
        if (!isPreviousDash)
        {
          sb.Append('-');
        }

        isPreviousDash = true;
      }
    }

    return sb.ToString();
  }
}