using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Sea_Battle
{
    public class ProfileData
    {
        public string nickname;
        public int winMatches;
        public int winRounds;
        public int loseMatches;
        public int loseRounds;

        private static string profilesFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DomikNaDerevi\\Sea-Battle\\profiles";

        public ProfileData()
        {
            nickname = "unknown";
            winRounds = 0;
            winMatches = 0;
            loseRounds = 0;
            loseMatches = 0;
        }

        public ProfileData(string nickname, int winRounds = 0, int winMatches = 0, int loseRounds = 0, int loseMatches = 0)
        {
            this.nickname = nickname;
            this.winRounds = winRounds;
            this.winMatches = winMatches;
            this.loseRounds = loseRounds;
            this.loseMatches = loseMatches;
        }

        public static void CheckPlayersFolder()
        {
            if (!Directory.Exists(profilesFolderPath))
                Directory.CreateDirectory(profilesFolderPath);
        }

        public static void SaveProfile(ProfileData profile)
        {
            CheckPlayersFolder();
            string path = PathForProfileWithNickname(profile.nickname);

            StreamWriter streamWriter = new StreamWriter(path);

            string json = JsonConvert.SerializeObject(profile);

            streamWriter.Write(json);

            streamWriter.Close();
        }

        public static ProfileData LoadProfile(string nickname)
        {
            CheckPlayersFolder();
            string path = PathForProfileWithNickname(nickname);
            try
            {
                ProfileData profile = JsonConvert.DeserializeObject<ProfileData>(File.ReadAllText(path));

                if (profile == null)
                    return new ProfileData();

                return profile;
            }
            catch
            {
                return new ProfileData();
            }
        }

        public static string PathForProfileWithNickname(string nickname)
            => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DomikNaDerevi\\Sea-Battle\\profiles\\" + nickname + ".json";

        public override string ToString()
            => $"nickname = {nickname}\nwinRounds = {winRounds}\nwinMatches = {winMatches}\nloseRounds = {loseRounds}\nloseMatches = {loseMatches}";

        public string GetDescription()
            => $"{nickname}\nyou won {winRounds} rounds and {winMatches} matches\nyou lost {loseRounds} rounds and {loseMatches} matches";

        public void WinMatch()
            => winMatches++;
        public void WinRound()
            => winRounds++;
        public void LoseMatch()
            => loseMatches++;
        public void LoseRound()
            => loseRounds++;
    }
}
