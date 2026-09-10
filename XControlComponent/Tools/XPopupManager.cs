using MaterialDesignThemes.Wpf;
using System.Windows.Controls.Primitives;
using XControlHelper;

namespace XControlComponents.Tools;

public static class XPopupManager
{
    public static List<Popup> OpenPopups { get; } = new List<Popup>();
    public static List<DialogHost> getAllDialogHost { get; } = new List<DialogHost>();
    public static Popup XComboBoxPopupOpened { get; set; }

    public static void CloseWhere(Func<Popup, bool> predicate)
    {
        foreach (var popup in OpenPopups.Where(predicate).ToList())
        {
            popup.IsOpen = false;
        }
    }

    public static void CloseByName(string Name)
    {
        var getPopup = OpenPopups.FirstOrDefault(a => a.Name == Name);
        getPopup.IsOpen = false;
    }

    #region Popup
    public static void RegisterPopup(Popup popup)
    {
        if (!OpenPopups.Contains(popup))
            OpenPopups.Add(popup);

        popup.IsOpen = true;

        // حذف خودکار از لیست هنگام بسته شدن
        popup.Closed += (s, e) => OpenPopups.Remove(popup);
    }


    public static void ClosePopupByTag(string Tag)
    {
        var getPopup = OpenPopups.FirstOrDefault(a => a.Tag.FillStringSafe() == Tag.FillStringSafe());
        if (getPopup != null)
            getPopup.IsOpen = false;
    }

    public static List<Popup> GetAllByOpened()
    {
        return OpenPopups.Where(a => a.IsOpen).ToList();
    }

    public static Popup GetPopupOpened()
    {
        return OpenPopups.FirstOrDefault(a => a.IsOpen);
    }

    public static void CloseAllPopups()
    {
        var getOpenedPopups = OpenPopups.Where(a => a.IsOpen).ToList();
        foreach (var popup in getOpenedPopups)
        {
            popup.IsOpen = false;
        }
    }
    #endregion

    #region DialogHost
    public static void RegisterDialogHost(DialogHost dialogHost)
    {
        if (!getAllDialogHost.Contains(dialogHost))
            getAllDialogHost.Add(dialogHost);

        dialogHost.IsOpen = true;

        // حذف خودکار از لیست هنگام بسته شدن
        dialogHost.DialogClosed += (s, e) => getAllDialogHost.Remove(dialogHost);
    }

    public static void CloseDialogHostByTag(string Tag)
    {
        var getDialogHost = getAllDialogHost.FirstOrDefault(a => a.Tag.FillStringSafe() == Tag.FillStringSafe());
        if (getDialogHost != null)
            getDialogHost.IsOpen = false;
    }

    public static void CloseAllDialogHosts()
    {
        var getOpenedDialogHosts = getAllDialogHost.Where(a => a.IsOpen).ToList();
        foreach (var dialogHost in getOpenedDialogHosts)
        {
            dialogHost.IsOpen = false;
        }
    }
    #endregion
}
