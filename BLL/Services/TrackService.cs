using BLL.DTO;
using BLL.Interfaces;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TrackService : ITrackService
    {
        private IDbRepos db;
        public TrackService(IDbRepos db)
        {
            this.db = db;
        }

        public bool SaveChanges()
        {
            return (db.Save() > 0) ? true : false;
        }

        public List<TrackDTO> GetTracks()
        {
            return db.Tracks.GetList().Select(i => new TrackDTO(i)).ToList();
        }

        public TrackDTO GetTrack(int id)
        {
            return new TrackDTO(db.Tracks.GetItem(id));
        }

        public void CreateTrack(TrackDTO track)
        {
            db.Tracks.Create(new Track() { Id = track.Id, CountHours = track.CountHours, DateTrack = track.DateTrack,
                                            IDTask = track.IDTask, IDWorker = track.IDWorker});
            SaveChanges();
        }

        public void UpdateTrack(TrackDTO track)
        {
            Track tr = db.Tracks.GetItem(track.Id);
            tr.CountHours = track.CountHours;
            tr.DateTrack = track.DateTrack;
            tr.IDWorker = track.IDWorker;
            tr.IDTask = track.IDTask;
            SaveChanges();
        }

        public void DeleteTrack(int id)
        {
            db.Tracks.Delete(id);
        }
    }
}
