function loadPostLikesDislikes() {

    $.ajax({
        url: '/api/Account/submitLikeDislike?postId=' + postId + '&isLike=' + isLike,
        headers: { 'Authorization': 'Bearer ' + localStorage.getItem('accessToken') },
        processData: false,
        contentType: false,
        success: function (e) {

            if ($('#' + postId + '.post-rating-positive')[0].src.includes('/liked.png')) removeLikeDislikePost(postId, true);
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

