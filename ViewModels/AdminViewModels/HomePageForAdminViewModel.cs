using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Models;

namespace Autosalon.ViewModels.AdminViewModels;

public class HomePageForAdminViewModel : ViewModelBase
{
    private ObservableCollection<UsersComments> GetReviewList()
    {
        using (var db = new AutosalonContext())
            return new ObservableCollection<UsersComments>(db.Comments.OrderBy(x => x.Date)
                .Join(db.Customers, c => c.CustomerId, u => u.Id,
                    (c, u) => new UsersComments { Customer = u, Comment = c }).ToList());
    }

    public HomePageForAdminViewModel()
    {
        Comments = GetReviewList();
        DeleteCommentCommand = new RelayCommand(OnDeleteCommentExecute, CanDeleteCommentExecute);
    }

    private ObservableCollection<UsersComments> _comments;
    private UsersComments? _selectedComment;

    public ObservableCollection<UsersComments> Comments
    {
        get => _comments;
        set => Set(ref _comments, value);
    }
    

    public UsersComments? SelectedComment
    {
        get => _selectedComment;
        set => Set(ref _selectedComment, value);
    }
    
    public ICommand DeleteCommentCommand { get; }
    private bool CanDeleteCommentExecute(object o) => true;
    private void OnDeleteCommentExecute(object o)
    {
        using (var db = new AutosalonContext())
        {
            var comment = db.Comments.FirstOrDefault(x => SelectedComment != null && x.Id == SelectedComment.Comment.Id);
            if (comment != null)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
            }
        }

        Comments = GetReviewList();
    }
}