$(document).on('click', '.clickable', function (e) {


    

    if ($(this)[0].attributes.post_id != null) {

        var postId = $(this)[0].attributes.post_id.textContent;
        if ($(this)[0].src.includes('/like.png')) {
            submitLikeDislikePost(postId, true );
        }
        else if ($(this)[0].src.includes('/liked.png')) {
            removeLikeDislikePost(postId, true);
        }
        else if ($(this)[0].src.includes('/dislike.png')) {
            submitLikeDislikePost(postId, false);
        }
        else if ($(this)[0].src.includes('/disliked.png')) {
            removeLikeDislikePost(postId, false);
        }
    } else {
        var comment_id = $(this)[0].attributes.comment_id.textContent;
        if ($(this)[0].src.includes('/like.png')) {
            submitLikeDislikeComment(comment_id, true);
        }
        else if ($(this)[0].src.includes('/liked.png')) {
            removeLikeDislikeComment(comment_id, true); 
        }
        else if ($(this)[0].src.includes('/dislike.png')) {
            submitLikeDislikeComment(comment_id, false);
        }
        else if ($(this)[0].src.includes('/disliked.png')) {
            removeLikeDislikeComment(comment_id, false);
        }
    }
});


function submitLikeDislikePost(postId, isLike) {
    $.ajax({
        type: 'post',
        url: '/api/Post/submitLikeDislike?postId=' + postId + '&isLike=' + isLike,
        headers: { 'Authorization': 'Bearer ' + localStorage.getItem('accessToken') },
        processData: false,
        contentType: false,
        success: function (e) {
            console.log('submied');
            if ($('#' + postId + '.post-rating-positive')[0].src.includes('/liked.png')) removeLikeDislikePost(postId, true );
            else if ($('#' + postId + '.post-rating-negative')[0].src.includes('/disliked.png')) removeLikeDislikePost(postId, false);

            if (isLike) {
                $('#' + postId + '.post-rating-positive')[0].src = '/Content/AppPhotos/liked.png';
                $('#' + postId + '.post-rating-positive-value')[0].textContent = parseInt($('#' + postId + '.post-rating-positive-value')[0].textContent) + 1;
            } else {
                $('#' + postId + '.post-rating-negative')[0].src = '/Content/AppPhotos/disliked.png';
                $('#' + postId + '.post-rating-negative-value')[0].textContent = parseInt($('#' + postId + '.post-rating-negative-value')[0].textContent) + 1;
            }
           
        },
        error: function (e) {
            console.log(e);
        }
    });
}
function removeLikeDislikePost(postId, wasLike) {
    $.ajax({
        type: 'post',
        url: '/api/Post/removeLikeDislike?postId=' + postId + '&wasLike=' + wasLike,
        headers: { 'Authorization': 'Bearer ' + localStorage.getItem('accessToken') },
        contentType : false,
        success: function (e) {
            console.log('removed');
            if (wasLike) {
                $('#' + postId + '.post-rating-positive')[0].src = '/Content/AppPhotos/like.png';
                $('#' + postId + '.post-rating-positive-value')[0].textContent = parseInt($('#' + postId + '.post-rating-positive-value')[0].textContent) - 1;
            }
            else {
                $('#' + postId + '.post-rating-negative')[0].src = '/Content/AppPhotos/dislike.png';
                $('#' + postId + '.post-rating-negative-value')[0].textContent = parseInt($('#' + postId + '.post-rating-negative-value')[0].textContent) - 1;
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}
function submitLikeDislikeComment(commentId, isLike) {
    $.ajax({
        type: 'post',
        url: '/api/Comment/submitLikeDislike?commentId=' + commentId + '&isLike=' + isLike,
        headers: { 'Authorization': 'Bearer ' + localStorage.getItem('accessToken') },
        processData: false,
        contentType: false,
        success: function (e) {
            console.log('submied');
            if ($('#' + commentId + '.comment-rating-positive')[0].src.includes('/liked.png')) removeLikeDislikeComment(commentId, true);
            else if ($('#' + commentId + '.comment-rating-negative')[0].src.includes('/disliked.png')) removeLikeDislikeComment(commentId, false);

            if (isLike) {
                $('#' + commentId + '.comment-rating-positive')[0].src = '/Content/AppPhotos/liked.png';
                $('#' + commentId + '.comment-rating-positive-value')[0].textContent = parseInt($('#' + commentId + '.comment-rating-positive-value')[0].textContent) + 1;
            } else {
                $('#' + commentId + '.comment-rating-negative')[0].src = '/Content/AppPhotos/disliked.png';
                $('#' + commentId + '.comment-rating-negative-value')[0].textContent = parseInt($('#' + commentId + '.comment-rating-negative-value')[0].textContent) + 1;
            }

        },
        error: function (e) {
            console.log(e);
        }
    });
}
function removeLikeDislikeComment(commentId, wasLike) {
    $.ajax({
        type: 'post',
        url: '/api/Comment/removeLikeDislike?commentId=' + commentId + '&wasLike=' + wasLike,
        headers: { 'Authorization': 'Bearer ' + localStorage.getItem('accessToken') },
        contentType: false,
        success: function (e) {
            console.log('removed');
            if (wasLike) {
                $('#' + commentId + '.comment-rating-positive')[0].src = '/Content/AppPhotos/like.png';
                $('#' + commentId + '.comment-rating-positive-value')[0].textContent = parseInt($('#' + commentId + '.comment-rating-positive-value')[0].textContent) - 1;
            }
            else {
                $('#' + commentId + '.comment-rating-negative')[0].src = '/Content/AppPhotos/dislike.png';
                $('#' + commentId + '.comment-rating-negative-value')[0].textContent = parseInt($('#' + commentId + '.comment-rating-negative-value')[0].textContent) - 1;
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}