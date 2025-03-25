using UnityEngine;

public class Notification
{
    private string _title;
    public string Title { get => _title; private set { } }

    private string _description;
    public string Description { get => _description; private set { } }

    private Sprite _icon;
    public Sprite Icon { get => _icon; private set { } }

    public enum Templates
    {
        Paper,
        Wood
    }

    private Templates _tempalte;
    public Templates Template { get => _tempalte; private set { } }

    public Notification(string title, string description, Sprite icon, Templates template)
    {
        _title = title;
        _description = description;
        _icon = icon;
        _tempalte = template;
    }

    public override string ToString()
    {
        return $"New notification: \n Title: {_title} \n Description: {_description} \n Icon: {_icon.name} \n Template: {_tempalte.ToString()}";
    }
}
