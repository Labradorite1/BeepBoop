using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeepBoopBot.Core.UserAccounts;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using NReco.ImageGenerator;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BeepBoopBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        string apiSite = "https://www.bungie.net/platform";
        string apiKey = Config.bot.apiToken;

        //User Commands
        [Command("Commands")]
        public async Task Commands()
        {
            string title = Utilities.GetFormattedAlert("Commands_Title");
            string description = Utilities.GetFormattedAlert("Commands_Description");
            string userCommands = Utilities.GetFormattedAlert("Commands_UserCommands");
            string userCommandsList = Utilities.GetFormattedAlert("Commands_UserCommandsList");
            string adminCommands = Utilities.GetFormattedAlert("Commands_AdminCommands");
            string adminCommandsList = Utilities.GetFormattedAlert("Commands_AdminCommandsList");

            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithColor(148, 0, 211);
            embed.WithDescription(description);
            embed.AddInlineField(userCommands, userCommandsList);
            embed.AddInlineField(adminCommands, adminCommandsList);

            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("Echo")]
        public async Task Echo([Remainder]string message)
        {
            string title = Utilities.GetFormattedAlert("Echo_Title");
            var embed = new EmbedBuilder();
            embed.WithTitle(Context.User.Mention + title);
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("Exotics")]
        public async Task Exotics([Remainder] string input = "vanilla")
        {
            string dlc = input.ToLower();
            string kinetic = Utilities.GetFormattedAlert("Exotics_Type00");
            string secondary = Utilities.GetFormattedAlert("Exotics_Type01");
            string heavy = Utilities.GetFormattedAlert("Exotics_Type02");
            string titan = Utilities.GetFormattedAlert("Exotics_Type03");
            string hunter = Utilities.GetFormattedAlert("Exotics_Type04");
            string warlock = Utilities.GetFormattedAlert("Exotics_Type05");
            string weapons = Utilities.GetFormattedAlert("Exotics_Type06");
            string armor = Utilities.GetFormattedAlert("Exotics_Type07");

            char pad = ' ';

            if (dlc == "vanilla")
            {
                string title = Utilities.GetFormattedAlert("Exotics_TitleVanilla");

                var exoticsVanilla = new List<string>();
                for (int i = 1; i <= 52; ++i)
                {
                    exoticsVanilla.Add(Utilities.GetFormattedAlert("Exotics_Vanilla" + i.ToString("00")));
                }

                var description1 = new StringBuilder()
                .AppendLine("```ini")
                .AppendLine("--------------------------------------------------------------------------------------------------------------")
                .AppendLine(kinetic.PadRight(30, pad) + secondary)
                .AppendLine(exoticsVanilla[00].PadRight(30, pad) + exoticsVanilla[05])
                .AppendLine(exoticsVanilla[01].PadRight(30, pad) + exoticsVanilla[06])
                .AppendLine(exoticsVanilla[02].PadRight(30, pad) + exoticsVanilla[07])
                .AppendLine(exoticsVanilla[03].PadRight(30, pad) + exoticsVanilla[08])
                .AppendLine(exoticsVanilla[04].PadRight(30, pad) + exoticsVanilla[09])
                .AppendLine(" ".PadRight(30, pad) + exoticsVanilla[10])
                .AppendLine(" ".PadRight(30, pad) + exoticsVanilla[11])
                .AppendLine(" ".PadRight(30, pad) + exoticsVanilla[12])
                .AppendLine(" ".PadRight(30, pad) + exoticsVanilla[13])
                .AppendLine(" ")
                .AppendLine(heavy)
                .AppendLine(exoticsVanilla[14])
                .AppendLine(exoticsVanilla[15])
                .AppendLine(exoticsVanilla[16])
                .AppendLine(exoticsVanilla[17])
                .Append(exoticsVanilla[18] + "```")
                .ToString();

                var description2 = new StringBuilder()
                .AppendLine("```ini")
                .AppendLine(titan.PadRight(30, pad) + hunter)
                .AppendLine(exoticsVanilla[19].PadRight(30, pad) + exoticsVanilla[30])
                .AppendLine(exoticsVanilla[20].PadRight(30, pad) + exoticsVanilla[31])
                .AppendLine(exoticsVanilla[21].PadRight(30, pad) + exoticsVanilla[32])
                .AppendLine(exoticsVanilla[22].PadRight(30, pad) + exoticsVanilla[33])
                .AppendLine(exoticsVanilla[23].PadRight(30, pad) + exoticsVanilla[34])
                .AppendLine(exoticsVanilla[24].PadRight(30, pad) + exoticsVanilla[35])
                .AppendLine(exoticsVanilla[25].PadRight(30, pad) + exoticsVanilla[36])
                .AppendLine(exoticsVanilla[26].PadRight(30, pad) + exoticsVanilla[37])
                .AppendLine(exoticsVanilla[27].PadRight(30, pad) + exoticsVanilla[38])
                .AppendLine(exoticsVanilla[28].PadRight(30, pad) + exoticsVanilla[39])
                .AppendLine(exoticsVanilla[29].PadRight(30, pad) + exoticsVanilla[40])
                .AppendLine(" ")
                .AppendLine(warlock)
                .AppendLine(exoticsVanilla[41])
                .AppendLine(exoticsVanilla[42])
                .AppendLine(exoticsVanilla[43])
                .AppendLine(exoticsVanilla[44])
                .AppendLine(exoticsVanilla[45])
                .AppendLine(exoticsVanilla[46])
                .AppendLine(exoticsVanilla[47])
                .AppendLine(exoticsVanilla[48])
                .AppendLine(exoticsVanilla[49])
                .AppendLine(exoticsVanilla[50])
                .AppendLine(exoticsVanilla[51])
                .AppendLine("--------------------------------------------------------------------------------------------------------------" + "```")
                .ToString();
                var embed = new EmbedBuilder();
                embed.WithTitle(title);
                embed.WithDescription(description1 + "\n" + description2);

                embed.WithColor(255, 211, 0);

                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            if (dlc == "curse of osiris")
            {
                string title = Utilities.GetFormattedAlert("Exotics_TitleCurseOfOsiris");

                var exoticsCurseOfOsiris = new List<string>();
                for (int i = 1; i <= 52; ++i)
                {
                    exoticsCurseOfOsiris.Add(Utilities.GetFormattedAlert("Exotics_CurseOfOsiris" + i.ToString("00")));
                }

                var description1 = new StringBuilder()
                .AppendLine("```ini")
                .AppendLine("--------------------------------------------------------------------------------------------------------------")
                .AppendLine(kinetic.PadRight(30, pad) + secondary)
                .AppendLine(exoticsCurseOfOsiris[00].PadRight(30, pad) + exoticsCurseOfOsiris[02])
                .AppendLine(exoticsCurseOfOsiris[01].PadRight(30, pad) + exoticsCurseOfOsiris[03])
                .AppendLine(" ")
                .AppendLine(heavy)
                .AppendLine(exoticsCurseOfOsiris[04] + "```")
                .ToString();

                var description2 = new StringBuilder()
                .AppendLine("```ini")
                .AppendLine(titan.PadRight(30, pad) + hunter)
                .AppendLine(exoticsCurseOfOsiris[05].PadRight(30, pad) + exoticsCurseOfOsiris[09])
                .AppendLine(exoticsCurseOfOsiris[06].PadRight(30, pad) + exoticsCurseOfOsiris[10])
                .AppendLine(exoticsCurseOfOsiris[07].PadRight(30, pad) + exoticsCurseOfOsiris[11])
                .AppendLine(exoticsCurseOfOsiris[08].PadRight(30, pad) + exoticsCurseOfOsiris[12])
                .AppendLine(" ")
                .AppendLine(warlock)
                .AppendLine(exoticsCurseOfOsiris[13])
                .AppendLine(exoticsCurseOfOsiris[14])
                .AppendLine(exoticsCurseOfOsiris[15])
                .AppendLine(exoticsCurseOfOsiris[16])
                .AppendLine("--------------------------------------------------------------------------------------------------------------" + "```")
                .ToString();

                var embed = new EmbedBuilder();
                embed.WithTitle(title);
                embed.WithDescription(description1 + "\n" + description2);
                embed.WithColor(255, 211, 0);

                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            if (dlc == "warmind")
            {
                string title = Utilities.GetFormattedAlert("Exotics_TitleWarmind");

                var exoticsWarmind = new List<string>();
                for (int i = 1; i <= 52; ++i)
                {
                    exoticsWarmind.Add(Utilities.GetFormattedAlert("Exotics_Warmind" + i.ToString("00")));
                }

                var description1 = new StringBuilder()
                .AppendLine("```ini")
                .AppendLine("--------------------------------------------------------------------------------------------------------------")
                .AppendLine(kinetic.PadRight(30, pad) + secondary)
                .AppendLine(exoticsWarmind[00].PadRight(30, pad) + exoticsWarmind[02])
                .AppendLine(exoticsWarmind[01])
                .AppendLine(" ")
                .AppendLine(heavy)
                .AppendLine(exoticsWarmind[03])
                .AppendLine(exoticsWarmind[04])
                .Append(exoticsWarmind[05] + "```")
                .ToString();

                var description2 = new StringBuilder()
                .AppendLine("```ini")
                .AppendLine(titan.PadRight(30, pad) + hunter)
                .AppendLine(exoticsWarmind[06].PadRight(30, pad) + exoticsWarmind[10])
                .AppendLine(exoticsWarmind[07].PadRight(30, pad) + exoticsWarmind[11])
                .AppendLine(exoticsWarmind[08].PadRight(30, pad) + exoticsWarmind[12])
                .AppendLine(exoticsWarmind[09].PadRight(30, pad) + exoticsWarmind[13])
                .AppendLine(" ")
                .AppendLine(warlock)
                .AppendLine(exoticsWarmind[14])
                .AppendLine(exoticsWarmind[15])
                .AppendLine(exoticsWarmind[16])
                .AppendLine(exoticsWarmind[17])
                .Append("--------------------------------------------------------------------------------------------------------------" + "```")
                .ToString();

                var embed = new EmbedBuilder();
                embed.WithTitle(title);
                embed.WithDescription(description1 + "\n" + description2);
                embed.WithColor(255, 211, 0);

                await Context.Channel.SendMessageAsync("", embed: embed);
            }
            if (dlc == "forsaken")
            {
                string title = Utilities.GetFormattedAlert("Exotics_TitleForsaken");

                var exoticsForsaken = new List<string>();
                for (int i = 1; i <= 52; ++i)
                {
                    exoticsForsaken.Add(Utilities.GetFormattedAlert("Exotics_Forsaken" + i.ToString("00")));
                }

                var description1 = new StringBuilder()
                .AppendLine("```ini")
                .AppendLine("--------------------------------------------------------------------------------------------------------------")
                .AppendLine(kinetic.PadRight(30, pad) + secondary)
                .AppendLine(exoticsForsaken[00].PadRight(30, pad) + exoticsForsaken[05])
                .AppendLine(exoticsForsaken[01].PadRight(30, pad) + exoticsForsaken[06])
                .AppendLine(exoticsForsaken[02].PadRight(30, pad) + exoticsForsaken[07])
                .AppendLine(exoticsForsaken[03])
                .AppendLine(exoticsForsaken[04])
                .AppendLine(" ")
                .AppendLine(heavy)
                .AppendLine(exoticsForsaken[08])
                .AppendLine(exoticsForsaken[09])
                .AppendLine(exoticsForsaken[10])
                .Append(exoticsForsaken[11] + "```")
                .ToString();

                var description2 = new StringBuilder()
                .AppendLine("```ini")
                .AppendLine(titan.PadRight(30, pad) + hunter)
                .AppendLine(exoticsForsaken[12].PadRight(30, pad) + exoticsForsaken[16])
                .AppendLine(exoticsForsaken[13].PadRight(30, pad) + exoticsForsaken[17])
                .AppendLine(exoticsForsaken[14].PadRight(30, pad) + exoticsForsaken[18])
                .AppendLine(exoticsForsaken[15].PadRight(30, pad) + exoticsForsaken[19])
                .AppendLine(" ")
                .AppendLine(heavy)
                .AppendLine(exoticsForsaken[20])
                .AppendLine(exoticsForsaken[21])
                .AppendLine(exoticsForsaken[22])
                .AppendLine(exoticsForsaken[23])
                .Append("--------------------------------------------------------------------------------------------------------------" + "```")
                .ToString();

                var embed = new EmbedBuilder();
                embed.WithTitle(title);
                embed.WithDescription(description1 + "\n" + description2);
                embed.WithColor(255, 211, 0);

                await Context.Channel.SendMessageAsync("", embed: embed);
            }
        }

        [Command("Exp")]
        public async Task Exp(uint exp)
        {
            string title = Utilities.GetFormattedAlert("Exp_Title");
            string description1 = Utilities.GetFormattedAlert("Exp_Description01");
            string description2 = Utilities.GetFormattedAlert("Exp_Description02");
            string description3 = Utilities.GetFormattedAlert("Exp_Description03");
            uint level = (uint)Math.Sqrt(Math.Sqrt(exp));
            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(description1 + exp + description2 + level + description3);
            embed.WithColor(0, 255, 0);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("Hello")]
        public async Task Hello()
        {
            string message = Utilities.GetFormattedAlert("Hello_Message");
            string punctuation = Utilities.GetFormattedAlert("Hello_Punctuation");
            string name = Context.User.Mention;
            await Context.Channel.SendMessageAsync(message + name + punctuation);
        }

        [Command("Help")]
        public async Task Help([Remainder] string command = "nocommand")
        {
            string lowerCommand = command.ToLower();
            string title = Utilities.GetFormattedAlert("Help_Title");
            string commandName = Utilities.GetFormattedAlert("Help_Title" + lowerCommand);
            string description = Utilities.GetFormattedAlert("Help_" + lowerCommand);

            var embed = new EmbedBuilder();
            embed.WithTitle(title + commandName);
            embed.WithDescription(description);
            embed.WithColor(253, 106, 2);

            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("Image")]
        public async Task Image(string background, string color, [Remainder] string message)
        {
            string confirmation = Utilities.GetFormattedAlert("Image_Confirmation");
            string fileName = Utilities.GetFormattedAlert("Image_FileName");
            string newline = "\n";
            string css = "<style>\n	{color: " + color + ";} \n body{background-image: url(\"https://www.bungie.net/common/destiny2_content/icons/40cf702a42a33d6d0050c632808045d3.jpg\"); } \n</style>";
            string html = String.Format($"<h1>" + message + "</h1>", newline);
            var converter = new HtmlToImageConverter
            {
                Width = 0,
                Height = 0,
            };
            var jpgBytes = converter.GenerateImage(css + html, NReco.ImageGenerator.ImageFormat.Jpeg);
            await Context.Channel.SendFileAsync(new MemoryStream(jpgBytes), message + fileName);
            await Context.Channel.SendMessageAsync(confirmation);
        }

        [Command("Lab")]
        public async Task Lab()
        {
            Random quotenumber = new Random();

            int Selection = quotenumber.Next(0, 5);

            string puns = Utilities.GetFormattedAlert("Lab_Quote" + Selection.ToString("00"));

            await Context.Channel.SendMessageAsync(puns);
        }

        [Command("Level")]
        public async Task Level(int level)
        {
            string title = Utilities.GetFormattedAlert("Level_Title");
            string description1 = Utilities.GetFormattedAlert("Level_Description01");
            string description2 = Utilities.GetFormattedAlert("Level_Description02");
            string description3 = Utilities.GetFormattedAlert("Level_Description03");
            uint exp = (uint)Math.Pow(Math.Pow(level, 2), 2);
            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(description1 + level + description2 + exp + description3);
            embed.WithColor(0, 255, 0);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("Pick")]
        public async Task Pick([Remainder]string message)
        {
            string title = Utilities.GetFormattedAlert("Pick_Title");
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle(title + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(255, 255, 0));
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("Poll")]
        public async Task Poll(string emoji, string emoji2, [Remainder] string question)
        {
            var sent = await Context.Channel.SendMessageAsync(question);
            RestUserMessage msg = sent;
            Global.MessageIdToTrack = msg.Id;
            Emoji emoji1 = new Emoji(emoji);
            Emoji emoji3 = new Emoji(emoji2);
            await sent.AddReactionAsync(emoji1);
            await sent.AddReactionAsync(emoji3);
            await Context.Message.DeleteAsync();
        }

        [Command("Pun")]
        public async Task Pun()
        {
            Random punnumber = new Random();

            int Selection = punnumber.Next(0, 33);

            string puns = Utilities.GetFormattedAlert("Pun_Pun" + Selection.ToString("00"));

            await Context.Channel.SendMessageAsync(puns);
        }

        [Command("React")]
        public async Task HandleReactionMessage()
        {
            string question = Utilities.GetFormattedAlert("React_Question");
            var sent = await Context.Channel.SendMessageAsync(question);
            RestUserMessage msg = sent;
            Global.MessageIdToTrack = msg.Id;
            var emoji = new Emoji("✔");
            var emoji2 = new Emoji("❌");
            await sent.AddReactionAsync(emoji);
            await sent.AddReactionAsync(emoji2);
        }

        [Command("Shaxx")]
        public async Task Shaxx()
        {
            Random shaxxQuote = new Random();
            int Selection = shaxxQuote.Next(0, 71);

            string title = Utilities.GetFormattedAlert("Shaxx_Title");
            string shaxx = Utilities.GetFormattedAlert("Shaxx_Quote" + Selection.ToString("00"));

            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(shaxx);
            embed.WithColor(new Color(255, 0, 0));

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("Stats")]
        public async Task Stats([Remainder]string arg = "")
        {
            string title = Utilities.GetFormattedAlert("Stats_Title");
            string points = Utilities.GetFormattedAlert("Stats_Points");
            string exp = Utilities.GetFormattedAlert("Stats_Exp");
            string level = Utilities.GetFormattedAlert("Stats_Level");
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;
            var account = UserAccounts.GetAccount(target);
            var embed = new EmbedBuilder();
            embed.WithTitle(target + title);
            embed.AddInlineField(points, account.Points);
            embed.AddInlineField(exp, account.EXP);
            embed.AddInlineField(level, account.LevelNumber);
            embed.WithColor(64, 224, 208);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("Themas")]
        public async Task Themas()
        {
            Random quotenumber = new Random();

            int Selection = quotenumber.Next(0, 5);

            string puns = Utilities.GetFormattedAlert("Themas_Quote" + Selection.ToString("00"));

            await Context.Channel.SendMessageAsync(puns);
        }

        //Admin Commands
        [Command("Warn")]
        [RequireUserPermission(GuildPermission.AddReactions)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task Warn(IGuildUser user, [Remainder] string reason)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.NumberOfWarnings++;
            UserAccounts.SaveAccounts();

            string title = Utilities.GetFormattedAlert("Warn_Title");
            string description = Utilities.GetFormattedAlert("Warn_Description");
            string punctuation = Utilities.GetFormattedAlert("Warn_Punctuation");
            string counter = Utilities.GetFormattedAlert("Warn_Counter");

            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(user.Mention + description + reason + punctuation);
            embed.WithColor(255, 255, 0);
            embed.AddInlineField(counter, userAccount.NumberOfWarnings);

            await Context.Channel.SendMessageAsync("", embed: embed);
            await Context.Message.DeleteAsync();
        }

        [Command("Unwarn")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Unwarn(IGuildUser user)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.NumberOfWarnings--;
            UserAccounts.SaveAccounts();

            string title = Utilities.GetFormattedAlert("Unwarn_Title");
            string description = Utilities.GetFormattedAlert("Unwarn_Description");
            string punctuation = Utilities.GetFormattedAlert("Unwarn_Punctuation");
            string counter = Utilities.GetFormattedAlert("Unwarn_Counter");

            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(description + user.Mention + punctuation);
            embed.WithColor(255, 255, 0);
            embed.AddInlineField(counter, userAccount.NumberOfWarnings);
            await Context.Channel.SendMessageAsync("", embed: embed);
            await Context.Message.DeleteAsync();
        }

        [Command("Mute")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Mute(IGuildUser user, [Remainder] string reason = "**No reason provided.**")
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.IsMuted = true;
            UserAccounts.SaveAccounts();

            string title = Utilities.GetFormattedAlert("Mute_Title");
            string description = Utilities.GetFormattedAlert("Mute_Description");
            string punctuation = Utilities.GetFormattedAlert("Mute_Punctuation");

            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(user.Mention + description + reason + punctuation);
            embed.WithColor(255, 140, 0);
            await Context.Channel.SendMessageAsync("", embed: embed);
            await Context.Message.DeleteAsync();
        }

        [Command("Unmute")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Unmute(IGuildUser user)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.IsMuted = false;
            UserAccounts.SaveAccounts();

            string title = Utilities.GetFormattedAlert("Unmute_Title");
            string description = Utilities.GetFormattedAlert("Unmute_Description");

            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(user.Mention + description);
            embed.WithColor(255, 140, 0);
            await Context.Channel.SendMessageAsync("", embed: embed);
            await Context.Message.DeleteAsync();
        }

        [Command("Kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task Kick(IGuildUser user, string reason = "**No reason provided**")
        {
            await user.KickAsync(reason);

            string title = Utilities.GetFormattedAlert("Kick_Title");
            string description = Utilities.GetFormattedAlert("Kick_Description");
            string punctuation = Utilities.GetFormattedAlert("Kick_Punctuation");

            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(user.Mention + description + reason + punctuation);
            embed.WithColor(255, 0, 0);
            await Context.Channel.SendMessageAsync("", embed: embed);
            await Context.Message.DeleteAsync();
        }

        [Command("Ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task Ban(IGuildUser user, [Remainder] string reason = "**No reason provided**")
        {
            await user.Guild.AddBanAsync(user, 5, reason);

            string title = Utilities.GetFormattedAlert("Ban_Title");
            string description = Utilities.GetFormattedAlert("Ban_Description");
            string punctuation = Utilities.GetFormattedAlert("Ban_Punctuation");

            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(user.Mention + description + reason + punctuation);
            embed.WithColor(255, 0, 0);
            await Context.Channel.SendMessageAsync("", embed: embed);
            await Context.Message.DeleteAsync();
        }

        [Command("AddEXP")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddEXP(uint EXP, [Remainder] string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;
            var account = UserAccounts.GetAccount(target);
            account.EXP += EXP;
            UserAccounts.SaveAccounts();

            string title = Utilities.GetFormattedAlert("AddExp_Title");
            string punctuation = Utilities.GetFormattedAlert("AddExp_Punctuation");
            string description1 = Utilities.GetFormattedAlert("AddExp_Description01");
            string description2 = Utilities.GetFormattedAlert("AddExp_Description02");

            var embed = new EmbedBuilder();
            embed.WithTitle(title + target + punctuation);
            embed.WithDescription(target + description1 + EXP + description2);
            embed.WithColor(255, 0, 255);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("AddPoints")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddPoints(uint Points, [Remainder] string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;
            var account = UserAccounts.GetAccount(target);
            account.Points += Points;
            UserAccounts.SaveAccounts();

            string title = Utilities.GetFormattedAlert("AddPoints_Title");
            string punctuation = Utilities.GetFormattedAlert("AddPoints_Punctuation");
            string description1 = Utilities.GetFormattedAlert("AddPoints_Description01");
            string description2 = Utilities.GetFormattedAlert("AddPoints_Description02");

            var embed = new EmbedBuilder();
            embed.WithTitle(title + target + punctuation);
            embed.WithDescription(target + description1 + Points + description2);
            embed.WithColor(255, 0, 255);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        //Useless Commands
        [Command("Secret")]
        public async Task Secret([Remainder]string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;

            string permissionsError = Utilities.GetFormattedAlert("Permission_Error");
            string title = Utilities.GetFormattedAlert("Secret_Title");
            string description = Utilities.GetFormattedAlert("Secret_Description");

            if (!UserIsSecretOwner((SocketGuildUser)target))
            {
                await Context.Channel.SendMessageAsync(permissionsError + target.Mention);
                return;
            }
            var dmChannel = await target.GetOrCreateDMChannelAsync();
            var embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(description);
            embed.WithColor(255, 0, 0);
            await dmChannel.SendMessageAsync("", embed: embed);
        }

        [Command("Spam")]
        public async Task Spam([Remainder]string arg = "")
        {
            var target = Context.Message.MentionedUsers.FirstOrDefault();
            var sender = Context.User;

            string permissionError = Utilities.GetFormattedAlert("Permission_Error");
            string spamStart = Utilities.GetFormattedAlert("Spam_Start");
            string spamComplete = Utilities.GetFormattedAlert("Spam_Complete");
            string description = Utilities.GetFormattedAlert("Spam_Description");

            if (!UserIsDeutchAmbassador((SocketGuildUser)sender))
            {
                await Context.Channel.SendMessageAsync(permissionError + sender.Mention);
                return;
            }
            await Context.Channel.SendMessageAsync(spamStart + target.Mention);
            var dmChannel = await target.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await dmChannel.SendMessageAsync(description);
            await Context.Channel.SendMessageAsync(spamComplete);
        }

        //Work In Progress Commands
        public async Task Unregistered()
        {
            string title = Utilities.GetFormattedAlert("Unregistered_Title");
            string message = Utilities.GetFormattedAlert("Unregistered_Message");
            var embed = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(message);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("Register")]
        public async Task Register(string platform, string username)
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;
            var account = UserAccounts.GetAccount(target);
            
            if (account.destinyMembershipId != " ")
            {
                await Context.Channel.SendMessageAsync("y0U h4Ve Alr3adY REg1sT3rEd");
                return;
            }
            if (account.destinyMembershipId == " ")
            {
                string platformLower = platform.ToLower();
                string membershipType = "-1";
                if (platformLower == "xbox")
                {
                    membershipType = "1";
                }
                if (platformLower == "playstation")
                {
                    membershipType = "2";
                }
                if (platformLower == "pc")
                {
                    membershipType = "4";
                }

                string destinyMembershipId = "0";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                    string link = apiSite + $"/Destiny2/SearchDestinyPlayer/{membershipType}/{username}/";
                    var response = await client.GetAsync(link);
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    var profiles = item.Response;
                    foreach (var profile in profiles)
                    {
                        destinyMembershipId = profile.membershipId;
                    }
                }

                string privacy = "0";
                string displayName = "Not found";
                List<string> characters = new List<string>();

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                    string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/?components=100";
                    var response = await client.GetAsync(link);
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    privacy = item.Response.profile.privacy;
                    displayName = item.Response.profile.data.userInfo.displayName;
                    var charactersList = item.Response.profile.data.characterIds;
                    foreach (string character in charactersList)
                    {
                        characters.Add(character);
                    }
                }
                if (privacy == "2")
                {
                    await Context.Channel.SendMessageAsync("User has set their profile to Private.");
                    return;
                }
                if (privacy == "1")
                { //Add displayName to this
                    account.destinyMembershipId = destinyMembershipId;
                    account.destinyMembershipType = membershipType;
                    account.character1 = characters[0];
                    account.character2 = characters[1];
                    account.character3 = characters[2];
                    UserAccounts.SaveAccounts();
                    string title = Utilities.GetFormattedAlert("Register_Title");
                    string description = Utilities.GetFormattedAlert("Register_Description");

                    var embed = new EmbedBuilder()
                        .WithTitle(title)
                        .WithDescription(description)
                        .AddInlineField("Display Name", displayName)
                        .AddInlineField("Destiny Membership Id", destinyMembershipId)
                        .AddInlineField("Destiny Membership Type", membershipType)
                        .AddInlineField("Character Id 1", characters[0])
                        .AddInlineField("Character Id 2", characters[1])
                        .AddInlineField("Character Id 3", characters[2]);
                    if (membershipType == "1")
                    {
                        embed.WithColor(0, 255, 0);
                    }
                    if (membershipType == "2")
                    {
                        embed.WithColor(0, 0, 255);
                    }
                    if (membershipType == "4")
                    {
                        embed.WithColor(255, 0, 0);
                    }
                    await Context.Channel.SendMessageAsync("", embed: embed);
                }
            }
        }

        [Command("Pve")]
        public async Task Pve([Remainder] string user = " ")
        {

            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;
            var account = UserAccounts.GetAccount(target);
            await Context.Channel.SendMessageAsync(user);

            if (account.destinyMembershipId == null)
            {
                await Unregistered();
            }

            string destinyMembershipId = account.destinyMembershipId;
            string membershipType = account.destinyMembershipType;
            string characterId = "0";
            List<string> characters = new List<string>();
            characters.Add(account.character1);
            characters.Add(account.character2);
            characters.Add(account.character3);

            await Context.Channel.SendMessageAsync("Thinking...");

            int completedNightfalls = 0;

            List<int> times = new List<int>();

            foreach (var x in characters)
            {
                string mode1 = "16";
                string mode2 = "17";
                string mode3 = "46";
                string mode4 = "47";
                List<string> modes = new List<string>();
                modes.Add(mode1);
                modes.Add(mode2);
                modes.Add(mode3);
                modes.Add(mode4);

                foreach (var mode in modes)
                {
                    int page = 0;
                    bool done = false;
                    while (done == false)
                    {
                        int totalNightfalls = 0;
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                            string link = apiSite + $"/Destiny2/{membershipType}/Account/{destinyMembershipId}/Character/{x}/Stats/Activities/?mode={mode}&count=250&page={page}";
                            var response = await client.GetAsync(link);
                            var content = await response.Content.ReadAsStringAsync();
                            dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                            var activities = item.Response.activities;
                            foreach (var activity in activities)
                            {
                                if (activity.values.activityDurationSeconds.basic.value == 314)
                                {
                                    Console.WriteLine(activity);
                                }
                                if (activity.values.completionReason.basic.displayValue != "Failed")
                                {
                                    if (activity.values.completed.basic.displayValue == "Yes")
                                    {
                                        completedNightfalls++;
                                        totalNightfalls++;
                                        int timesValue = activity.values.activityDurationSeconds.basic.value;
                                        times.Add(timesValue);
                                    }
                                    if (activity.values.completed.basic.displayValue == "No")
                                    {
                                        totalNightfalls++;
                                    }
                                }
                                if (activity.values.completionReason.basic.displayValue == "Failed")
                                {
                                    totalNightfalls++;
                                }
                            }   
                        }
                        page++;
                        if (totalNightfalls < 250)
                        {
                            done = true;
                        }
                    }
                }
            }

            int tempFastestTime = 604800;
            int fastestTime = 0;

            foreach (var time in times)
            {
                if (tempFastestTime > time)
                {
                    fastestTime = time;
                    tempFastestTime = time;
                }
            }

            int fastestTimeMinutes = fastestTime / 60;
            int fastestTimeMintesRemainder = fastestTime % 60;

            string completedNightfallsText = completedNightfalls.ToString();

            string totalPveTime = "";
            string highestPower = "";
            string publicEvents = "";
            string totalStrikeTime = "";
            string strikes = "";
            string totalRaidTime = "";
            string totalRaids = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Account/{destinyMembershipId}/Character/{characterId}/Stats/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                string error = item.ErrorCode;
                Console.WriteLine(error);
                await Context.Channel.SendMessageAsync("Almost there...");

                int patrolTime = item.Response.patrol.allTime.secondsPlayed.basic.value;
                int raidTime = item.Response.raid.allTime.secondsPlayed.basic.value;
                int storyTime = item.Response.story.allTime.secondsPlayed.basic.value;
                int allStrikesTime = item.Response.allStrikes.allTime.secondsPlayed.basic.value;
                int totalTime = patrolTime + raidTime + storyTime + allStrikesTime;

                int totalDaysPlayed = totalTime / 86400;
                int totalDaysPlayedRemainder = totalTime % 86400;

                int totalHoursPlayed = totalDaysPlayedRemainder / 3600;
                int totalHoursPlayedRemainder = totalDaysPlayedRemainder % 3600;

                int totalMinutesPlayed = totalHoursPlayedRemainder / 60;
                int totalMinutesPlayedRemainder = totalHoursPlayedRemainder % 60;

                totalPveTime = $"{totalDaysPlayed}d {totalHoursPlayed}h {totalMinutesPlayed}m {totalMinutesPlayedRemainder}s";

                int strikeDaysPlayed = allStrikesTime / 86400;
                int strikeDaysPlayedRemainder = allStrikesTime % 86400;

                int strikeHoursPlayed = strikeDaysPlayedRemainder / 3600;
                int strikeHoursPlayedRemainder = strikeDaysPlayedRemainder % 3600;

                int strikeMinutesPlayed = strikeHoursPlayedRemainder / 60;
                int strikeMinutesPlayedRemainder = strikeHoursPlayed % 60;

                totalStrikeTime = $"{strikeDaysPlayed}d {strikeHoursPlayed}h {strikeMinutesPlayed}m {strikeMinutesPlayedRemainder}s";
                highestPower = item.Response.patrol.allTime.highestLightLevel.basic.displayValue;

                int patrolPublicEvents = item.Response.patrol.allTime.publicEventsCompleted.basic.value;
                int strikePublicEvents = item.Response.allStrikes.allTime.publicEventsCompleted.basic.value;
                int totalPublicEvents = patrolPublicEvents + strikePublicEvents;

                int heroicPatrolPublicEvents = item.Response.patrol.allTime.heroicPublicEventsCompleted.basic.value;
                int heroicStrikePublicEvents = item.Response.allStrikes.allTime.heroicPublicEventsCompleted.basic.value;
                int totalHeroicPublicEvents = heroicPatrolPublicEvents + heroicStrikePublicEvents;

                int smallPercentageHeroicPublicEvents = 100000 / totalPublicEvents;
                int percentageHeroicPublicEvents1 = smallPercentageHeroicPublicEvents * totalHeroicPublicEvents;
                int percentageHeroicPublicEvents = percentageHeroicPublicEvents1 / 1000;


                publicEvents = $"{totalPublicEvents} ({percentageHeroicPublicEvents}% Heroic)";

                string totalStrikes = item.Response.allStrikes.allTime.activitiesCleared.basic.displayValue; //total time

                string fastestStrike = item.Response.allStrikes.allTime.fastestCompletionMs.basic.displayValue; //fastest strike

                strikes = $"{totalStrikes} ({fastestStrike})";

                int raidDaysPlayed = raidTime / 86400;
                int raidDaysPlayedRemainder = raidTime % 86400;

                int raidHoursPlayed = raidDaysPlayedRemainder / 3600;
                int raidHoursPlayedRemainder = raidDaysPlayedRemainder % 3600;

                int raidMinutesPlayed = raidHoursPlayedRemainder / 60;
                int raidMinutesPlayedRemainder = raidHoursPlayedRemainder % 60; //raid time 
                totalRaidTime = $"{raidDaysPlayed}d {raidHoursPlayed}h {raidMinutesPlayed}m {raidMinutesPlayedRemainder}s";

                totalRaids = item.Response.raid.allTime.activitiesCleared.basic.displayValue; //total raids 
            }
            string characterCount;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Account/{destinyMembershipId}/Stats/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                var characters1 = item.Response.characters;

                int deletedCharacters = 0;
                int activeCharacters = 0;
                foreach (var character in characters1)
                {
                    string deleted = character.deleted;

                    if (deleted == "True")
                    {
                        deletedCharacters++;
                    }
                    if (deleted == "False")
                    {
                        activeCharacters++;
                    }
                }
                characterCount = $"{activeCharacters} / {deletedCharacters}";
            }
            string title = Utilities.GetFormattedAlert("PvE_Title");
            string totalTimePlayedPve = Utilities.GetFormattedAlert("PvE_TotalTimePlayed");
            string powerLevel = Utilities.GetFormattedAlert("PvE_PowerLevel");
            string publicEventsCompleted = Utilities.GetFormattedAlert("PvE_PublicEventsCompleted");
            string totalTimePlayedStrikes = Utilities.GetFormattedAlert("PvE_TotalTimePlayedStrikes");
            string totalStrikesCompleted = Utilities.GetFormattedAlert("PvE_TotalStrikesCompleted");
            string totalNightfallsCompleted = Utilities.GetFormattedAlert("PvE_TotalNightfallsCompleted");
            string totalTimePlayedRaid = Utilities.GetFormattedAlert("PvE_TotalTimePlayedRaid");
            string totalRaidsCompleted = Utilities.GetFormattedAlert("PvE_TotalRaidsCompleted");
            string characterCountText = Utilities.GetFormattedAlert("PvE_CharacterCount");
            var embed = new EmbedBuilder()
                .WithTitle(title)
                .AddInlineField(totalTimePlayedPve, totalPveTime)
                .AddInlineField(powerLevel, highestPower)
                .AddInlineField(publicEventsCompleted, publicEvents)
                .AddInlineField(totalTimePlayedStrikes, totalStrikeTime)
                .AddInlineField(totalStrikesCompleted, strikes)
                .AddInlineField(totalNightfallsCompleted, completedNightfallsText + $" ({fastestTimeMinutes}m {fastestTimeMintesRemainder}s)")
                .AddInlineField(totalTimePlayedRaid, totalRaidTime)
                .AddInlineField(totalRaidsCompleted, totalRaids)
                .AddInlineField(characterCountText, characterCount);
            if (membershipType == "1")
            {
                embed.WithColor(0, 255, 0);
            }
            if (membershipType == "2")
            {
                embed.WithColor(0, 0, 255);
            }
            if (membershipType == "4")
            {
                embed.WithColor(255, 0, 0);
            }

            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("Time")]
        public async Task Time([Remainder] string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;
            var account = UserAccounts.GetAccount(target);

            if (account.destinyMembershipId == null)
            {
                await Unregistered();
            }

            string destinyMembershipId = account.destinyMembershipId;
            string membershipType = account.destinyMembershipType;
            int deletedCharacters = 0;
            int activeCharacters = 0;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Account/{destinyMembershipId}/Stats/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                var characters = item.Response.characters;
                foreach (var character in characters)
                {
                    string deleted = character.deleted;

                    if (deleted == "True")
                    {
                        deletedCharacters++;
                    }
                    if (deleted == "False")
                    {
                        activeCharacters++;
                    }
                }
            }
        }

        [Command("GetProfile")]
        public async Task GetProfile([Remainder] string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;
            var account = UserAccounts.GetAccount(target);

            if (account.destinyMembershipId == null)
            {
                await Unregistered();
            }

            string destinyMembershipId = account.destinyMembershipId;
            string membershipType = account.destinyMembershipType;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/?components=100,101,102,103,104,200,201,202,203,204,205,300,301,302,303,304,305,306,307,308,400,401,402,500,600,700,800,900";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                Console.WriteLine(item.ErrorCode);

                File.WriteAllText("SystemLang / response.txt", content);
                /*foreach (var x in message)
                {
                    await Context.Channel.SendMessageAsync(x);
                }*/
            }
        }

        [Command("GetItem")]
        public async Task GetItems([Remainder] string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser ?? Context.User;
            var account = UserAccounts.GetAccount(target);

            if (account.destinyMembershipId == null)
            {
                await Unregistered();
            }

            string destinyMembershipId = account.destinyMembershipId;
            string membershipType = account.destinyMembershipType;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/?components=102,201";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                Console.WriteLine(item.ErrorCode);
                List<string> characters = new List<string>();
                characters.Add(account.character1);
                characters.Add(account.character2);
                characters.Add(account.character3);

                foreach (var character in characters)
                {
                    await Context.Channel.SendMessageAsync(character);
                }
            }
        }

        [Command("Id")] //endpoint : Destiny 2, GET, Search Destiny Player, gets the ID the platform uses to identify the player.
        public async Task Id(string platform, string username)
        {
            using (var client = new HttpClient())
            {
                string platformLower = platform.ToLower();
                string membershipType = "-1";
                if (platformLower == "xbox")
                {
                    membershipType = "1";
                }
                if (platformLower == "playstation")
                {
                    membershipType = "2";
                }
                if (platformLower == "pc")
                {
                    membershipType = "4";
                }

                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/SearchDestinyPlayer/{membershipType}/{username}/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                string description = item.Response[0].membershipId;

                string title = Utilities.GetFormattedAlert("Id_Title");
                string title2 = Utilities.GetFormattedAlert("Id_Title2");
                var embed = new EmbedBuilder();
                embed.WithTitle(title + username + title2 + platform);
                embed.WithDescription("`" + description + "`");
                embed.WithColor(0, 255, 0);
                await Context.Channel.SendMessageAsync("", embed: embed);
                string errorCode = item.ErrorCode;
                Console.WriteLine("Error code: " + errorCode);
            }
        }
            
    

        [Command("Info")]
        public async Task Info(string username)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/User/SearchUsers/?q={username}";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                var message = item.Response;
                foreach (var user in message)
                {
                    string membershipId = user.membershipId;
                    string displayName = user.displayName;
                    string profilePicture = "https://bungie.net" + user.profilePicturePath;

                    string psnDisplayName = user.psnDisplayName;
                    if (user.psnDisplayName == null)
                    {
                        psnDisplayName = "Account not linked.";
                    }

                    string xboxDisplayName = user.xboxDisplayName;
                    if (user.xboxDisplayName == null)
                    {
                        xboxDisplayName = "Account not linked.";
                    }

                    string blizzardDisplayName = user.blizzardDisplayName;
                    if (user.blizzardDisplayName == null)
                    {
                        blizzardDisplayName = "Account not linked.";
                    }

                    string firstAccess = user.firstAccess;
                    if (user.firstAccess == null)
                    {
                        firstAccess = "Not found.";
                    }

                    string lastAccess = user.lastUpdate;
                    if (user.lastUpdate == null)
                    {
                        lastAccess = "Not found";
                    }

                    var embed = new EmbedBuilder();
                    embed.WithTitle("User info");
                    embed.AddInlineField("Membership Id:", membershipId);
                    embed.AddInlineField("Bungie.net Username:", displayName);
                    embed.AddInlineField("First Forum Activity:", firstAccess);
                    embed.AddInlineField("Last Forum Activity:", lastAccess);
                    embed.AddInlineField("Playstation Username:", psnDisplayName);
                    embed.AddInlineField("Xbox Username:", xboxDisplayName);
                    embed.AddInlineField("Blizzard Username:", blizzardDisplayName);
                    embed.WithThumbnailUrl(profilePicture);
                    embed.WithColor(0, 0, 255);

                    await Context.Channel.SendMessageAsync("", embed: embed);
                }
            }
        }

        [Command("GetContent")]
        public async Task GetContent(string membershipType, string destinyMembershipId, string characterId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Account/{destinyMembershipId}/Character/{characterId}/Stats/AggregateActivityStats/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                Console.WriteLine(content);
            }
        }

        [Command("CharacterId")] //endpoint : Destiny 2, GET, get profile
        public async Task CharacterId(string platform, string username)
        {
            string displayName = username;
            string platformLower = platform.ToLower();
            string membershipType = "1";
            if (platformLower == "xbox")
            {
                membershipType = "1";
            }
            if (platformLower == "playstation")
            {
                membershipType = "2";
            }
            if (platformLower == "pc")
            {
                membershipType = "4";
                displayName = username.Replace("#", "%23");
            }

            string destinyMembershipId = "Not found";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/SearchDestinyPlayer/{membershipType}/{displayName}/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                destinyMembershipId = item.Response[0].membershipId;
            }

            string characterId01 = "Not found.";
            string characterId02 = "Not found.";
            string characterId03 = "Not found.";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey); //gets the characterIds using the membership type and the user id
                string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/?components=100";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                characterId01 = item.Response.profile.data.characterIds[0];
                characterId02 = item.Response.profile.data.characterIds[1];
                characterId03 = item.Response.profile.data.characterIds[2];
            }

            string character01Number = "3";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/Character/{characterId01}/?components=100,200";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                character01Number = item.Response.character.data.classType;
            }

            string character02Number = "3";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/Character/{characterId02}/?components=100,200";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                character02Number = item.Response.character.data.classType;
            }

            string character03Number = "3";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/Character/{characterId03}/?components=100,200";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                character03Number = item.Response.character.data.classType;
            }

            string character01Class;
            character01Class = "3   ";
            if (character01Number == "0")
            {
                character01Class = "Titan";
            }
            if (character01Number == "1")
            {
                character01Class = "Hunter";
            }
            if (character01Number == "2")
            {
                character01Class = "Warlock";
            }
            if (character01Number == "3")
            {
                character01Class = "Unknown";
            }
            string character02Class;
            character02Class = "3";
            if (character02Number == "0")
            {
                character02Class = "Titan";
            }
            if (character02Number == "1")
            {
                character02Class = "Hunter";
            }
            if (character02Number == "2")
            {
                character02Class = "Warlock";
            }
            if (character02Number == "3")
            {
                character02Class = "Unknown";
            }

            string character03Class;
            character03Class = "3";
            if (character03Number == "0")
            {
                character03Class = "Titan";
            }
            if (character03Number == "1")
            {
                character03Class = "Hunter";
            }
            if (character03Number == "2")
            {
                character03Class = "Warlock";
            }
            if (character03Number == "3")
            {
                character03Class = "Unknown";
            }

            string title = Utilities.GetFormattedAlert("CharacterId_Title");
            string title2 = Utilities.GetFormattedAlert("CharacterId_Title2");

            var embed = new EmbedBuilder();
            embed.WithTitle(title + username + title2 + platform);
            embed.WithDescription("**" + character01Class + "** \n`" + characterId01 + "`\n" + "**" + character02Class + "** \n`" + characterId02 + "`\n" + "**" + character03Class + "** \n`" + characterId03 + "`");
            embed.WithColor(0, 0, 255);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("CharactersImage")]
        public async Task CharactersImage(string platform, string username)
        {
            string displayName = username;
            string platformLower = platform.ToLower();
            string membershipType = "1";
            if (platformLower == "xbox")
            {
                membershipType = "1";
            }
            if (platformLower == "playstation")
            {
                membershipType = "2";
            }
            if (platformLower == "pc")
            {
                membershipType = "4";
                displayName = username.Replace("#", "%23");
            }

            string destinyMembershipId = " ";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey); //gets the destinyMembershipId using the username and the membership type
                string link = apiSite + $"/Destiny2/SearchDestinyPlayer/{membershipType}/{displayName}/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                destinyMembershipId = item.Response[0].membershipId;
            }

            JArray characterId = new JArray(); //gets the character ids and puts them into the array
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/?components=100";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                characterId = item.Response.profile.data.characterIds;
            }



            foreach (var id in characterId)
            {
                int pveKills = 0;
                int pveDeaths = 0;
                int timeSpentInPve = 0;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                    string link = apiSite + $"/Destiny2/{membershipType}/Account/{destinyMembershipId}/Character/{id}/Stats/";
                    var response = await client.GetAsync(link);
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                    int pveKills1 = item.Response.patrol.allTime.kills.basic.displayValue;
                    int pveKills2 = item.Response.raid.allTime.kills.basic.displayValue;
                    int pveKills3 = item.Response.story.allTime.kills.basic.displayValue;
                    int pveKills4 = item.Response.allStrikes.allTime.kills.basic.displayValue;
                    pveKills = pveKills1 + pveKills2 + pveKills3 + pveKills4;

                    int pveDeaths1 = item.Response.patrol.allTime.deaths.basic.displayValue;
                    int pveDeaths2 = item.Response.raid.allTime.deaths.basic.displayValue;
                    int pveDeaths3 = item.Response.story.allTime.deaths.basic.displayValue;
                    int pveDeaths4 = item.Response.allStrikes.allTime.deaths.basic.displayValue;
                    pveDeaths = pveDeaths1 + pveDeaths2 + pveDeaths3 + pveDeaths4;

                    int timeSpentInPve1 = item.Response.patrol.allTime.secondsPlayed.basic.value;
                    int timeSpentInPve2 = item.Response.raid.allTime.secondsPlayed.basic.value;
                    int timeSpentInPve3 = item.Response.story.allTime.secondsPlayed.basic.value;
                    int timeSpentInPve4 = item.Response.allStrikes.allTime.secondsPlayed.basic.value;
                    timeSpentInPve = timeSpentInPve1 + timeSpentInPve2 + timeSpentInPve3 + timeSpentInPve4;
                }

                int daysPlayedPve = timeSpentInPve / 86400;
                int daysPlayedPveRemainder = timeSpentInPve % 86400;

                int hoursPlayedPve = daysPlayedPveRemainder / 3600;
                int hoursPlayedPveRemainder = daysPlayedPveRemainder % 3600;

                int minutesPlayedPve = hoursPlayedPveRemainder / 60;
                int minutesPlayedPveRemainder = hoursPlayedPveRemainder % 60;


                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                    string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/Character/{id}/?components=100,200";
                    var response = await client.GetAsync(link);
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                    string membershipIdText = item.Response.character.data.membershipId;
                    string membershipTypeText = item.Response.character.data.membershipType;
                    string characterIdText = item.Response.character.data.characterId;
                    string dateLastPlayedText = item.Response.character.data.dateLastPlayed;
                    string minutesPlayedThisSessionText = item.Response.character.data.minutesPlayedThisSession;
                    int minutesPlayedTotalText = item.Response.character.data.minutesPlayedTotal;
                    string lightText = item.Response.character.data.light;
                    string emblemBackgroundPathText = "https://www.bungie.net" + item.Response.character.data.emblemBackgroundPath;
                    string percentToNextLevelText = item.Response.character.data.percentToNextLevel;
                    string baseCharacterLevelText = item.Response.character.data.baseCharacterLevel;
                    string emblemPathText = "https://www.bungie.net" + item.Response.character.data.emblemPath;
                    string classTypeText = item.Response.character.data.classType;
                    string genderTypeText = item.Response.character.data.genderType;
                    string raceTypeText = item.Response.character.data.raceType;

                    int dividentDays = 1440;
                    int daysPlayed = minutesPlayedTotalText / dividentDays;
                    int remainderDaysPlayed = minutesPlayedTotalText % dividentDays;

                    int dividentHours = 60;
                    int hoursPlayed = remainderDaysPlayed / dividentHours;
                    int remainderHoursPlayed = remainderDaysPlayed % dividentHours;


                    string backgroundUrl = "";
                    string characterClass = "Not defined";
                    if (classTypeText == "0")
                    {
                        characterClass = "Titan";
                        backgroundUrl = "https://i.imgur.com/FrDvvNm.jpg";
                    }
                    if (classTypeText == "1")
                    {
                        characterClass = "Hunter";
                        backgroundUrl = "https://i.imgur.com/qJFcOjf.png";
                        ;
                    }
                    if (classTypeText == "2")
                    {
                        characterClass = "Warlock";
                        backgroundUrl = "https://i.imgur.com/PT88MEg.jpg";
                    }

                    string gender = "Not defined";
                    if (genderTypeText == "0")
                    {
                        gender = "Male";
                    }
                    if (genderTypeText == "1")
                    {
                        gender = "Female";
                    }

                    string race = "Not defined";
                    if (raceTypeText == "0")
                    {
                        race = "Human";
                    }
                    if (raceTypeText == "1")
                    {
                        race = "Awoken";
                    }
                    if (raceTypeText == "2")
                    {
                        race = "Exo";
                    }
                    string fileName = Utilities.GetFormattedAlert("Image_FileName");
                    string css = "<style> body {background-image: url(" + backgroundUrl + "); } p.a{ text-align: left; font-style: italic; color:rgb(0,0,0); } p.b{ text-align: right; font-style: italic;} </style>";
                    string html = String.Format("<h1 class=\"font\" >" + username + "'s " + characterClass + "</h1> " +
                        "<p class=\"a\">Character info: " + race + " " + gender + "</p>" +
                        "<p class=\"a\">Current light: " + lightText + "</p>" +
                        "<p class=\"a\">Character level: " + baseCharacterLevelText + "</p>" +
                        "<p class=\"a\">Time played: " + daysPlayed + "d " + hoursPlayed + "h " + remainderHoursPlayed + "m </p>" +
                        "<p class=\"a\">Date last played: " + dateLastPlayedText + "</p>" +
                        "<p class=\"a\">Minutes last session: " + minutesPlayedThisSessionText + "</p>" +
                        "<p class=\"a\">PvE Kills: " + pveKills + "</p>" +
                        "<p class=\"a\">PvE Deaths: " + pveDeaths + "</p>" +
                        "<p class=\"a\">Time spent in PvE: " + daysPlayedPve + "d " + hoursPlayedPve + "h " + minutesPlayedPve + "m " + minutesPlayedPveRemainder + "s" + "</p>"
                        );
                    var converter = new HtmlToImageConverter
                    {
                        Width = 960,
                        Height = 540,
                    };
                    var jpgBytes = converter.GenerateImage(css + html, NReco.ImageGenerator.ImageFormat.Jpeg);
                    await Context.Channel.SendFileAsync(new MemoryStream(jpgBytes), characterClass + fileName);
                }
            }
        }

        [Command("PvPStats")]
        public async Task PvPStats(string characterId, string destinyMembershipId, string membershipType)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = $"https://www.bungie.net/platform/Destiny2/{membershipType}/Account/{destinyMembershipId}/Character/{characterId}/Stats/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                string activitiesEntered = item.Response.allPvP.allTime.activitiesEntered.basic.displayValue;
                string activitiesWon = item.Response.allPvP.allTime.activitiesWon.basic.displayValue;
                string assists = item.Response.allPvP.allTime.assists.basic.displayValue;
                string totalDeathDistance = item.Response.allPvP.allTime.totalDeathDistance.basic.displayValue;
                string averageDeathDistance = item.Response.allPvP.allTime.averageDeathDistance.basic.displayValue;
                string totalKillDistance = item.Response.allPvP.allTime.totalKillDistance.basic.displayValue;
                string kills = item.Response.allPvP.allTime.kills.basic.displayValue;
                string averageKillDistance = item.Response.allPvP.allTime.averageKillDistance.basic.displayValue;
                string secondsPlayed = item.Response.allPvP.allTime.secondsPlayed.basic.displayValue;
                string deaths = item.Response.allPvP.allTime.deaths.basic.displayValue;
                string averageLifespan = item.Response.allPvP.allTime.averageLifespan.basic.displayValue;
                string score = item.Response.allPvP.allTime.score.basic.displayValue;
                string averageScorePerKill = item.Response.allPvP.allTime.averageScorePerKill.basic.displayValue;
                string averageScorePerLife = item.Response.allPvP.allTime.averageScorePerLife.basic.displayValue;
                string bestSingleGameKills = item.Response.allPvP.allTime.bestSingleGameKills.basic.displayValue;
                string bestSingleGameScore = item.Response.allPvP.allTime.bestSingleGameScore.basic.displayValue;
                string opponentsDefeated = item.Response.allPvP.allTime.opponentsDefeated.basic.displayValue;
                string efficiency = item.Response.allPvP.allTime.efficiency.basic.displayValue;
                string killsDeathsRatio = item.Response.allPvP.allTime.killsDeathsRatio.basic.displayValue;
                string killsDeathsAssists = item.Response.allPvP.allTime.killsDeathsAssists.basic.displayValue;
                string objectivesCompleted = item.Response.allPvP.allTime.objectivesCompleted.basic.displayValue;
                string precisionKills = item.Response.allPvP.allTime.precisionKills.basic.displayValue;
                string resurrectionsPerformed = item.Response.allPvP.allTime.resurrectionsPerformed.basic.displayValue;
                string resurrectionsReceived = item.Response.allPvP.allTime.resurrectionsReceived.basic.displayValue;
                string suicides = item.Response.allPvP.allTime.suicides.basic.displayValue;
                string weaponKillsAutoRifle = item.Response.allPvP.allTime.weaponKillsAutoRifle.basic.displayValue;
                string weaponKillsBeamRifle = item.Response.allPvP.allTime.weaponKillsBeamRifle.basic.displayValue;
                string weaponKillsBow = item.Response.allPvP.allTime.weaponKillsBow.basic.displayValue;
                string weaponKillsFusionRifle = item.Response.allPvP.allTime.weaponKillsFusionRifle.basic.displayValue;
                string weaponKillsHandCannon = item.Response.allPvP.allTime.weaponKillsHandCannon.basic.displayValue;
                string weaponKillsTraceRifle = item.Response.allPvP.allTime.weaponKillsTraceRifle.basic.displayValue;
                string weaponKillsPulseRifle = item.Response.allPvP.allTime.weaponKillsPulseRifle.basic.displayValue;
                string weaponKillsRocketLauncher = item.Response.allPvP.allTime.weaponKillsRocketLauncher.basic.displayValue;
                string weaponKillsScoutRifle = item.Response.allPvP.allTime.weaponKillsScoutRifle.basic.displayValue;
                string weaponKillsShotgun = item.Response.allPvP.allTime.weaponKillsShotgun.basic.displayValue;
                string weaponKillsSniper = item.Response.allPvP.allTime.weaponKillsSniper.basic.displayValue;
                string weaponKillsSubmachinegun = item.Response.allPvP.allTime.weaponKillsSubmachinegun.basic.displayValue;
                string weaponKillsRelic = item.Response.allPvP.allTime.weaponKillsRelic.basic.displayValue;
                string weaponKillsSideArm = item.Response.allPvP.allTime.weaponKillsSideArm.basic.displayValue;
                string weaponKillsSword = item.Response.allPvP.allTime.weaponKillsSword.basic.displayValue;
                string weaponKillsAbility = item.Response.allPvP.allTime.weaponKillsAbility.basic.displayValue;
                string weaponKillsGrenade = item.Response.allPvP.allTime.weaponKillsGrenade.basic.displayValue;
                string weaponKillsGrenadeLauncher = item.Response.allPvP.allTime.weaponKillsGrenadeLauncher.basic.displayValue;
                string weaponKillsSuper = item.Response.allPvP.allTime.weaponKillsSuper.basic.displayValue;
                string weaponKillsMelee = item.Response.allPvP.allTime.weaponKillsMelee.basic.displayValue;
                string weaponBestType = item.Response.allPvP.allTime.weaponBestType.basic.displayValue;
                string winLossRatio = item.Response.allPvP.allTime.winLossRatio.basic.displayValue;
                string allParticipantsCount = item.Response.allPvP.allTime.allParticipantsCount.basic.displayValue;
                string allParticipantsScore = item.Response.allPvP.allTime.allParticipantsScore.basic.displayValue;
                string allParticipantsTimePlayed = item.Response.allPvP.allTime.allParticipantsTimePlayed.basic.displayValue;
                string longestKillSpree = item.Response.allPvP.allTime.longestKillSpree.basic.displayValue;
                string longestSingleLife = item.Response.allPvP.allTime.longestSingleLife.basic.displayValue;
                string mostPrecisionKills = item.Response.allPvP.allTime.mostPrecisionKills.basic.displayValue;
                string orbsDropped = item.Response.allPvP.allTime.orbsDropped.basic.displayValue;
                string orbsGathered = item.Response.allPvP.allTime.orbsGathered.basic.displayValue;
                string remainingTimeAfterQuitSeconds = item.Response.allPvP.allTime.remainingTimeAfterQuitSeconds.basic.displayValue;
                string teamScore = item.Response.allPvP.allTime.teamScore.basic.displayValue;
                string totalActivityDurationSeconds = item.Response.allPvP.allTime.totalActivityDurationSeconds.basic.displayValue;
                string combatRating = item.Response.allPvP.allTime.combatRating.basic.displayValue;
                string fastestCompletionMs = item.Response.allPvP.allTime.fastestCompletionMs.basic.displayValue;
                string longestKillDistance = item.Response.allPvP.allTime.longestKillDistance.basic.displayValue;
                string highestCharacterLevel = item.Response.allPvP.allTime.highestCharacterLevel.basic.displayValue;
                string highestLightLevel = item.Response.allPvP.allTime.highestLightLevel.basic.displayValue;
                string fireTeamActivities = item.Response.allPvP.allTime.fireTeamActivities.basic.displayValue;

                var embed = new EmbedBuilder()
                    .WithTitle("PvP stats")
                    .AddInlineField("Activities entered", activitiesEntered)
                    .AddInlineField("Activities won", activitiesWon)
                    .AddInlineField("Assists", assists)
                    .AddInlineField("Total death distance", totalDeathDistance)
                    .AddInlineField("Average death distance", averageDeathDistance)
                    .AddInlineField("Total kill distance", totalKillDistance)
                    .AddInlineField("Kills", kills)
                    .AddInlineField("Average kill distance", averageKillDistance)
                    .AddInlineField("Time played", secondsPlayed)
                    .AddInlineField("Deaths", deaths)
                    .AddInlineField("Average lifespan", averageLifespan)
                    .AddInlineField("Score", score)
                    .AddInlineField("Average score per kill", averageScorePerKill)
                    .AddInlineField("Average score per life", averageScorePerLife)
                    .AddInlineField("Best single game kills", bestSingleGameKills)
                    .AddInlineField("Best single game score", bestSingleGameScore)
                    .AddInlineField("Opponents defeated", opponentsDefeated)
                    .AddInlineField("Efficiency", efficiency)
                    .AddInlineField("Kill death ratio", killsDeathsRatio)
                    .AddInlineField("Kill death assists", killsDeathsAssists)
                    .AddInlineField("Objectives completed", objectivesCompleted)
                    .AddInlineField("Precision kills", precisionKills)
                    .AddInlineField("Revives performed", resurrectionsPerformed)
                    .AddInlineField("Revives recieved", resurrectionsReceived);

                var embed2 = new EmbedBuilder()
                    .AddInlineField("Suicides", suicides)
                    .AddInlineField("Auto rifle kills", weaponKillsAutoRifle)
                    .AddInlineField("Beam rifle kills", weaponKillsBeamRifle)
                    .AddInlineField("Bow kills", weaponKillsBow)
                    .AddInlineField("Fusion rifle kills", weaponKillsFusionRifle)
                    .AddInlineField("Hand cannon kills", weaponKillsHandCannon)
                    .AddInlineField("Trace rifle kills", weaponKillsTraceRifle)
                    .AddInlineField("Pulse rifle kills", weaponKillsPulseRifle)
                    .AddInlineField("Rocket launcher kills", weaponKillsRocketLauncher)
                    .AddInlineField("Scout rifle kills", weaponKillsScoutRifle)
                    .AddInlineField("Shotgun kills", weaponKillsShotgun)
                    .AddInlineField("Sniper kills", weaponKillsSniper)
                    .AddInlineField("Submachinegun kills", weaponKillsSubmachinegun)
                    .AddInlineField("Relic kills", weaponKillsRelic)
                    .AddInlineField("Sidearm kills", weaponKillsSideArm)
                    .AddInlineField("Sword kills", weaponKillsSword)
                    .AddInlineField("Ability kills", weaponKillsAbility)
                    .AddInlineField("Grenade kills", weaponKillsGrenade)
                    .AddInlineField("Grenade launcher kills", weaponKillsGrenadeLauncher)
                    .AddInlineField("Super kills", weaponKillsSuper)
                    .AddInlineField("Melee kills", weaponKillsMelee)
                    .AddInlineField("Best weapon", weaponBestType)
                    .AddInlineField("Win loss ratio", winLossRatio)
                    .AddInlineField("All participants count", allParticipantsCount)
                    .AddInlineField("All participants score", allParticipantsScore);

                var embed3 = new EmbedBuilder()
                    .AddInlineField("All participants time played", allParticipantsTimePlayed)
                    .AddInlineField("Longest kill spree", longestKillSpree)
                    .AddInlineField("Longest single life", longestSingleLife)
                    .AddInlineField("Most precision kills", mostPrecisionKills)
                    .AddInlineField("Orbs generated", orbsDropped)
                    .AddInlineField("Orbs consumed", orbsGathered)
                    .AddInlineField("Remaining time after quitting", remainingTimeAfterQuitSeconds)
                    .AddInlineField("Team score", teamScore)
                    .AddInlineField("Total activity time", totalActivityDurationSeconds)
                    .AddInlineField("Combat rating", combatRating)
                    .AddInlineField("Fastest completeion", fastestCompletionMs)
                    .AddInlineField("Longest kill distance", longestKillDistance)
                    .AddInlineField("Highest character level", highestCharacterLevel)
                    .AddInlineField("Highest light level", highestLightLevel)
                    .AddInlineField("Activities as fireteam", fireTeamActivities)
                    ;

                await Context.Channel.SendMessageAsync("", embed: embed);
                await Context.Channel.SendMessageAsync("", embed: embed2);
                await Context.Channel.SendMessageAsync("", embed: embed3);
            }
        }

        [Command("Characters")] //endpoint : Destiny 2, GET, get profile
        public async Task Characters(string platform, string username)
        {
            string displayName = username;
            string platformLower = platform.ToLower();
            string membershipType = "1";
            if (platformLower == "xbox")
            {
                membershipType = "1";
            }
            if (platformLower == "playstation")
            {
                membershipType = "2";
            }
            if (platformLower == "pc")
            {
                membershipType = "4";
                displayName = username.Replace("#", "%23");
            }

            string destinyMembershipId = " ";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey); //gets the destinyMembershipId using the username and the membership type
                string link = apiSite + $"/Destiny2/SearchDestinyPlayer/{membershipType}/{displayName}/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                destinyMembershipId = item.Response[0].membershipId;
            }

            JArray characterId = new JArray(); //gets the character ids and puts them into the array
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/?components=100";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                characterId = item.Response.profile.data.characterIds;
            }

            foreach (var id in characterId)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                    string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/Character/{id}/?components=100,200";
                    var response = await client.GetAsync(link);
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                    string membershipIdText = item.Response.character.data.membershipId;
                    string membershipTypeText = item.Response.character.data.membershipType;
                    string characterIdText = item.Response.character.data.characterId;
                    string dateLastPlayedText = item.Response.character.data.dateLastPlayed;
                    string minutesPlayedThisSessionText = item.Response.character.data.minutesPlayedThisSession;
                    int minutesPlayedTotalText = item.Response.character.data.minutesPlayedTotal;
                    string lightText = item.Response.character.data.light;
                    string emblemBackgroundPathText = "https://www.bungie.net" + item.Response.character.data.emblemBackgroundPath;
                    string percentToNextLevelText = item.Response.character.data.percentToNextLevel;
                    string baseCharacterLevelText = item.Response.character.data.baseCharacterLevel;
                    string emblemPathText = "https://www.bungie.net" + item.Response.character.data.emblemPath;
                    string classTypeText = item.Response.character.data.classType;
                    string genderTypeText = item.Response.character.data.genderType;
                    string raceTypeText = item.Response.character.data.raceType;

                    int dividentDays = 1440;
                    int daysPlayed = minutesPlayedTotalText / dividentDays;
                    int remainderDaysPlayed = minutesPlayedTotalText % dividentDays;

                    int dividentHours = 60;
                    int hoursPlayed = remainderDaysPlayed / dividentHours;
                    int remainderHoursPlayed = remainderDaysPlayed % dividentHours;

                    string characterClass = "Not defined";
                    if (classTypeText == "0")
                    {
                        characterClass = "Titan";
                    }
                    if (classTypeText == "1")
                    {
                        characterClass = "Hunter";
                    }
                    if (classTypeText == "2")
                    {
                        characterClass = "Warlock";
                    }

                    string gender = "Not defined";
                    if (genderTypeText == "0")
                    {
                        gender = "Male";
                    }
                    if (genderTypeText == "1")
                    {
                        gender = "Female";
                    }

                    string race = "Not defined";
                    if (raceTypeText == "0")
                    {
                        race = "Human";
                    }
                    if (raceTypeText == "1")
                    {
                        race = "Awoken";
                    }
                    if (raceTypeText == "2")
                    {
                        race = "Exo";
                    }

                    var embed = new EmbedBuilder()
                        .WithTitle($"**" + username + "'s " + characterClass + "**")
                        .AddInlineField("Membership id", membershipIdText)
                        .AddInlineField("Membership type", membershipTypeText)
                        .AddInlineField("Character id", characterIdText)
                        .AddInlineField("Last played", dateLastPlayedText)
                        .AddInlineField("Minutes last session", minutesPlayedThisSessionText)
                        .AddInlineField("Hours played", daysPlayed + "d " + hoursPlayed + "h " + remainderHoursPlayed + "m")
                        .AddInlineField("Current light", lightText)
                        .AddInlineField("% until next level", percentToNextLevelText)
                        .AddInlineField("Character level", baseCharacterLevelText)
                        .AddInlineField("Character class", race + " " + gender)
                        .WithThumbnailUrl(emblemPathText);
                    Console.WriteLine(emblemBackgroundPathText);
                    await Context.Channel.SendMessageAsync("", embed: embed);
                }
            }
        }

        [Command("Inventory")]
        public async Task Inventory(string characterId, string destinyMembershipId, string membershipType)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/Character/{characterId}/?components=100,101,102,103,104,200,201,202,203,204,205,300,301,302,303,304,305,306,307,308,400,401,402,500,600,700,800,900";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                string message = item.Response.data.items[0];
                await Context.Channel.SendMessageAsync(message);
            }
        }

        [Command("Hash")]
        public async Task Hash()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = "https://www.bungie.net/Platform/Destiny2/Manifest/DestinyRaceDefinition/3887404748/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                Console.WriteLine(content);
            }
        }

        [Command("Characters1")] //endpoint : Destiny 2, GET, get profile
        public async Task Characters1(string platform, string username)
        {
            string displayName = username;
            string platformLower = platform.ToLower();
            string membershipType = "1";
            if (platformLower == "xbox")
            {
                membershipType = "1";
            }
            if (platformLower == "playstation")
            {
                membershipType = "2";
            }
            if (platformLower == "pc")
            {
                membershipType = "4";
                displayName = username.Replace("#", "%23");
            }

            string destinyMembershipId = " ";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey); //gets the destinyMembershipId using the username and the membership type
                string link = apiSite + $"/Destiny2/SearchDestinyPlayer/{membershipType}/{displayName}/";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                destinyMembershipId = item.Response[0].membershipId;
            }

            JArray characterId = new JArray(); //gets the character ids and puts them into the array
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/?components=100";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                characterId = item.Response.profile.data.characterIds;
            }

            List<string> message = new List<string>();

            foreach (var id in characterId)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                    string link = apiSite + $"/Destiny2/{membershipType}/Profile/{destinyMembershipId}/Character/{id}/?components=100,200";
                    var response = await client.GetAsync(link);
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                    string message00 = item.Response.character.data.membershipId;
                    string message01 = item.Response.character.data.membershipType;
                    string message02 = item.Response.character.data.characterId;
                    string message03 = item.Response.character.data.dateLastPlayed;
                    string message04 = item.Response.character.data.minutesPlayedThisSession;
                    string message05 = item.Response.character.data.minutesPlayedTotal;
                    string message06 = item.Response.character.data.light;
                    string message07 = item.Response.character.data.emblemBackgroundPath;
                    string message08 = item.Response.character.data.percentToNextLevel;
                    string message09 = item.Response.character.data.baseCharacterLevel;


                    var description = new StringBuilder()
                        .AppendLine("```ini")
                        .AppendLine("[Membership Id]" + "\n" + message00 + "\n")
                        .AppendLine("[Membership Type]" + "\n" + message01 + "\n")
                        .AppendLine("[Character Id]" + "\n" + message02 + "\n")
                        .AppendLine("[Last played]" + "\n" + message03 + "\n")
                        .AppendLine("[Minutes played last sessions]" + "\n" + message04 + "\n")
                        .AppendLine("[Minutes played total]" + "\n" + message05 + "\n")
                        .AppendLine("[Current light]" + "\n" + message06 + "\n")
                        .AppendLine("[% until next level]" + "\n" + message08 + "\n")
                        .AppendLine("[Character level]" + "\n" + message09 + "\n")
                        .Append("```")
                        .ToString();

                    string url = "https://www.bungie.net" + message07;

                    message.Add(url);
                    message.Add(description);
                }
            }

            var embed = new EmbedBuilder();
            embed.WithTitle("Character information");
            embed.AddInlineField("Character 1: ", message[1]);
            embed.AddInlineField("Character 2: ", message[3]);
            embed.AddInlineField("Character 3: ", message[5]);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }
        /*
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
                string link = apiSite +$"";
                var response = await client.GetAsync(link);
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
            }
        */
        //Permissions (roles)

        private bool UserIsSecretOwner(SocketGuildUser user)
        {
            string targetRoleName = "SecretOwner";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }

        private bool UserIsDeutchAmbassador(SocketGuildUser user)
        {
            string targetRoleName = "Deutch Ambassador";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
    }
    //Crap
    /*
    [Command("Person")]
    public async Task GetRandomPerson()
    {
        string json = "";
        using (WebClient client = new WebClient())
        {
            json = client.DownloadString("https://randomuser.me/api/");
        }
        var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

        string firstName = dataObject.results[0].name.first.ToString();
        string lastName = dataObject.results[0].name.last.ToString();
        string avatarURL = dataObject.results[0].picture.large.ToString();
        string generatedPerson = Utilities.GetFormattedAlert("Person_Title");
        string personFirst = Utilities.GetFormattedAlert("Person_First");
        string personLast = Utilities.GetFormattedAlert("Person_Last");

        var embed = new EmbedBuilder();
        embed.WithThumbnailUrl(avatarURL);
        embed.WithTitle(generatedPerson);
        embed.AddInlineField(personFirst, firstName);
        embed.AddInlineField(personLast, lastName);

        await Context.Channel.SendMessageAsync("", embed: embed);
    }*/
    /*
    [Command("Test")]
    public async Task Test([Remainder] string arg = "")
    {
        SocketUser target = null;
        var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
        target = mentionedUser ?? Context.User;
        var account = UserAccounts.GetAccount(target);

        if (account.destinyMembershipId == null)
        {
            await Unregistered();
        }

        string membershipType = account.destinyMembershipType;
        string destinyMembershipId = account.destinyMembershipId;
        string characterId = "0";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
            string link = $"https://www.bungie.net/platform/Destiny2/{membershipType}/Account/{destinyMembershipId}/Character/{characterId}/Stats/";
            var response = await client.GetAsync(link);
            var content = await response.Content.ReadAsStringAsync();
            dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
            string activitiesEntered = item.Response.allPvP.allTime.activitiesEntered.basic.displayValue;
            await Context.Channel.SendMessageAsync(activitiesEntered);
        }
    }*/


    /*
    [Command("Data")]
    public async Task Data()
    {
        await Context.Channel.SendMessageAsync("Data has " + DataStorage.GetPairsCount() + " pairs.");
    }

    [Command("Edit")]
    public async Task Edit([Remainder]string message)
    {
        string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        DataStorage.AddPairToStorage(options[0], options[1]);
        await Context.Channel.SendMessageAsync("The command: " + options[0] + "has succesfully been updated."); 
    }

    [Command("Clear")]
    public async Task Clear()
    {
        DataStorage.DeleteData();
        await Context.Channel.SendMessageAsync("The content has been deleted.");
        DataStorage.ValidateStorageFile("DataStorage.json");
    }
    */
}
