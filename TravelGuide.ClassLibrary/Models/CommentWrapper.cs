using TravelGuide.Database.Entities;

namespace TravelGuide.ClassLibrary.Models
{
    public class CommentWrapper
    {
        #region Declarations

        private Comment comment;
        private string username;

        #endregion

        #region Constructor

        public CommentWrapper(Comment comment, string username)
        {
            Comment = comment;
            Username = username;
        }

        public CommentWrapper()
        {

        }

        #endregion

        #region Properties

        public Comment Comment
        {
            get => comment;

            set => comment = value;
        }

        public string Username
        {
            get => username;

            set => username = value;
        }

        #endregion
    }
}
