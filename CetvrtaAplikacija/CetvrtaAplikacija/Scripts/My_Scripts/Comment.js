function PopulateComments(postId) {
    $.ajax({
        url: '/api/comment?postId=' + postId,
        success: function (data) {
            //console.log('populating comments for ' + postId);
            console.log(data);
            var commentSection = $('<div id="' + postId + '" class="row commentSection" style="margin:2em 0;"></div>');

            var headLineRow = $('<div class="row" style="border-top:1px solid #ececec;margin:0;padding:0;"></div>');
            headLineRow.append($('<div class="col-md-4"></div>'));
            headLineRow.append($('<div class="col-md-4"><h5 style="margin-top:-0.5em;background-color:white;text-align:center;">Comments</h5></div>'));
            headLineRow.append($('<div class="col-md-4" style="text-align:right;">'
                + '<div class= "btn-group" style = "margin:-1.5em 0 0 0;background-color:white;padding:0 0.5em 0 0.5em;">'
                + '<button class="btn btn-default btn-xs dropdown-toggle" role="button" data-toggle="dropdown" style="display:inline;"><span class="glyphicon glyphicon-sort" style="font-size:0.8em;"></span></button>'
                + '<ul class="dropdown-menu" aria-labelledby="navbarDropdownSort" style="">'
                + '<li id="sortCommentsByRatingSubmit" class="dropdown-item"><a href="#" style="font-size:0.8em;">By rating</a></li>'
                + '<li id = "sortCommentsByUploadDateSubmit" class= "dropdown-item" > <a href="#" style="font-size:0.8em;">By upload date</a></li>'
                + '</ul></div></div>'));
            commentSection.append(headLineRow);


            var submitCommentRow = $('<div class="form-inline" style="margin:2em 0 2em 0;">'
                + '<input id="' + postId + '" class="form-control input-sm submit-comment-input" style="width:75%;margin-right:1em;" placeholder ="Submit comment"/>'
                + '<button id="' + postId + '" class="btn btn-default btn-sm submit-comment-button" style="">Submit</button>'
                + '</div>');
            commentSection.append(submitCommentRow);

            var commentsContainer = $('<div></div>');
            if (data.Comments.length > 0) {
                for (var i = 0; i < data.Comments.length; i++) {
                    var current = data.Comments[i];
                    var singleCommentContainer = $('<div id="' + current.Id + '" class="row" createdVal="'+current.Created+'" ratingVal="'+(current.Likes - current.Dislikes)+'" style="margin:0;padding:0;"></div>');

                    singleCommentContainer.append($('<div class="col-md-1" style="margin:0;padding:0;">'
                        + '<img src="/Content/UserPhotos/' + current.Owner.Icon + '" class="hoverable clickable" ownerId="' + current.Owner.Id + '" style="max-width:2em;"/>'
                        + '</div>'));

                    singleCommentContainer.append($('<div class="col-md-10">'
                        + '<div class="row" style="margin:0;padding:0;"><p style="font-size:0.5em;opacity:0.9;margin:0;padding:0;">' + current.CreatedString + '</p></div>'
                        + '<div class="row" style="margin:0;padding:0;"><p style="margin:0;padding:0;">' + current.Content + '</p></div>'
                        + '<div class="row" style="margin:0;padding:0;">'
                        + '<div style="display:inline;"><img id = "' + current.Id + '" src = "/Content/AppPhotos/like.png" class="hoverable clickable comment-rating-positive" comment_id="' + current.Id + '" style="width:100%;max-width:0.75em;" > <p id="' + current.Id + '" class="comment-rating-positive-value" style="display:inline;font-size:0.7em;">' + current.Likes + '</p></img ></div>'
                        + '<div style="display:inline;margin-left:0.5em;"><img id = "' + current.Id + '" src = "/Content/AppPhotos/dislike.png" class="hoverable clickable comment-rating-negative" comment_id="' + current.Id + '" style="width:100%;max-width:0.75em;" > <p id="' + current.Id + '" class="comment-rating-negative-value" style="display:inline;font-size:0.7em;">' + current.Likes + '</p></img ></div >'
                            + '</div>'
                        + '</div>'));


                    singleCommentContainer.append($('<div class="col-md-1"></div>'));
                    commentsContainer.append(singleCommentContainer);
                }
            }
            commentSection.append(commentsContainer);
            $('#' + postId + '.singlePostDiv').append(commentSection);
        },
        error: function (errorMsg) {
            console.log(errorMsg);
        }
    });
}

$(document).on('click', '.submit-comment-button',function (e) {
    //console.log('eeeee');
    createComment($(this)[0].id);
});

function createComment(postId) {


    var commentContent = $('#' + postId + '.submit-comment-input').val();
    var cData = new FormData();
    cData.append('postId', postId);
    cData.append('content', commentContent);

    $.ajax({
        type: 'post',
        url: '/api/Comment/Create',
        headers: { 'Authorization': 'Bearer ' + localStorage.getItem('accessToken') },
        data: cData,
        contentType: false,
        processData: false,
        
        success: function (e) {
            console.log(e);
        },
        error: function (e) {
            console.log(e);
        }
    });

}