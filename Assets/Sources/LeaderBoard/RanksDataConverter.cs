using Sources.Ui;
using UnityEngine;

namespace Sources.LeaderBoard
{
    public static class RanksDataConverter
    {
        private const string AvatarPath = "Sprite/Avatar";
        private const string FlagPath = "Sprite/Flag";

        public static RanksData FromYandex(string name, int rank, int score, string lang, string avatarName)
        {
            Debug.Log($"name: {name}, rank: {rank}, score: {score}, lang: {lang}, avatarName: {avatarName}");
            Sprite avatar = Resources.Load<Sprite>($"{AvatarPath}/{avatarName}");
            Sprite flag = Resources.Load<Sprite>($"{FlagPath}/{lang}");
            return new RanksData(rank, score, avatar, name, flag);
        }
    }
}