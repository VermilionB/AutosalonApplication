using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Models;

namespace Autosalon.ViewModels;

public class AboutViewModel : ViewModelBase
{
    private ObservableCollection<UsersComments> GetReviewList()
    {
        using (var db = new AutosalonContext())
            return new ObservableCollection<UsersComments>(db.Comments.OrderBy(x => x.Date)
                .Join(db.Customers, c => c.CustomerId, u => u.Id,
                    (c, u) => new UsersComments { Customer = u, Comment = c }).ToList());
    }

    public AboutViewModel()
    {
        Comments = GetReviewList();
        AddReviewCommand = new RelayCommand(OnAddReviewExecute, CanAddReviewExecuted);
    }

    private string? _commentText;
    private ObservableCollection<UsersComments> _comments;

    public ObservableCollection<UsersComments> Comments
    {
        get => _comments;
        set => Set(ref _comments, value);
    }

    public string? CommentText
    {
        get => _commentText;
        set => Set(ref _commentText, value);
    }

    public ICommand AddReviewCommand { get; }
    private bool CanAddReviewExecuted(object o) => true;

    private void OnAddReviewExecute(object o)
    {
        Comment comment = new Comment();
        comment.Id = Guid.NewGuid();
        comment.Date = DateTime.Now;
        comment.CustomerId = CurrentUser.getInstanceCustomer().Id;
        comment.Text = CommentText;

        using (var db = new AutosalonContext())
        {
            db.Comments.Add(comment);
            db.SaveChanges();
        }

        Comments = GetReviewList();
    }
        
}