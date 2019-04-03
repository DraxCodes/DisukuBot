using Discord;
using Discord.Commands;
using DisukuBot.DisukuCore.Services.TMDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord.Modules
{
    public class TMDB : ModuleBase<SocketCommandContext>
    {
        private TmdbService _tmdbService;
        private string _logo = "https://www.themoviedb.org/assets/2/v4/logos/293x302-powered-by-square-green-3ee4814bb59d8260d51efdd7c124383540fc04ca27d23eaea3a8c87bfa0f388d.png";

        public TMDB(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [Command("Movie")]
        public async Task GetMovie([Remainder]string search)
        {
            var result = await _tmdbService.GetMovieAsync(search);
            var embed = new EmbedBuilder()
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithColor(Color.Blue)
                .WithUrl(result.Url)
                .WithImageUrl(result.BackdropUrl)
                .WithFooter($"Release Date: {result.ReleaseDate.ToShortDateString()}", _logo)
                .WithThumbnailUrl(result.ImageUrl);

            await ReplyAsync(embed: embed.Build());
        }

        //TODO: Refactor the format of the embed.
        [Command("MCollection")]
        public async Task GetCollection([Remainder]string search)
        {
            var collection = await _tmdbService.GetMovieCollectionAsync(search);
            var sb = new StringBuilder();
            var embed = new EmbedBuilder()
                .WithTitle($"Collection For: {search.ToUpper()}")
                .WithThumbnailUrl(_logo)
                .WithColor(Color.Blue);

            foreach (var movie in collection)
            {
                sb.Append($"[{movie.Title}]({movie.Url}) - ({movie.ReleaseDate.Year})\n");
                //var description = movie.Description.Remove(40, movie.Description.Length - 40);
                //sb.Append($"Description: {description}\n");
            }

            embed.WithDescription(sb.ToString());

            await ReplyAsync(embed: embed.Build());
        }
    }
}
