using Life;
using Life.Network;
using Life.UI;

namespace SMairie
{
    public class SMairie : Plugin
    {
        public SMairie(IGameAPI aPI) : base(aPI) { }

        public override void OnPluginInit()
        {
            base.OnPluginInit();
            new SChatCommand("/mairie", "Permet d'envoyer un message à la mairie", "/mairie", (player, args) =>
            {
                if (!player.IsAdmin || !player.serviceAdmin)
                {
                    player.Notify(nameof(SMairie), "Vous ne faites pas partie de la mairie ou n'êtes pas en service.", NotificationManager.Type.Warning);
                    return;
                }

                UIPanel panel = new UIPanel(nameof(SMairie), UIPanel.PanelType.Input);

                panel.SetInputPlaceholder("Message...");

                panel.AddButton("Fermer", _ => player.ClosePanel(panel));
                panel.AddButton("Envoyer", delegate
                {
                    string str = panel.inputText;
                    if (string.IsNullOrEmpty(str))
                    {
                        player.Notify(nameof(SMairie), "Annonce invalide !", NotificationManager.Type.Warning);
                        return;
                    }

                    player.ClosePanel(panel);
                    Nova.server.SendMessageToAll("<color=#3cbdcf>[SMairie] : </color>" + str);
                    player.Notify(nameof(SMairie), "Annonce mairie envoyée avec succès !");
                });

                player.ShowPanelUI(panel);
            }).Register();
        }
    }
}
