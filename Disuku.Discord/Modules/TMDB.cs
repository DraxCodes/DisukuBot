using Discord.Commands;
using Disuku.Core.Services.TMDB;
using System.Threading.Tasks;

namespace Disuku.Discord.Modules
{
    public class TMDB : ModuleBase<SocketCommandContext>
    {
        private readonly ITmdbService _tmdbService;

        public TMDB(ITmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [Command("Movie")]
        public async Task GetMovie([Remainder]string search)
            => await _tmdbService.ReplyMovieAsync(Context.Channel.Id, search);

        [Command("MCollection")]
        public async Task GetCollection([Remainder]string search)
            => await _tmdbService.ReplyMovieCollectionAsync(Context.Channel.Id, search);

        [Command("TvShow")]
        public async Task GetTvShow([Remainder]string search)
            => await _tmdbService.ReplyTvShowAsync(Context.Channel.Id, search);
    }
}
