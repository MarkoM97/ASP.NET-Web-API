function PopulatePosts() {
    $.ajax({
        url: '/api/post',
        //headers: {
        //    'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
        //},
        success: function (data) {
            for (var i = 0; i < data.Posts.length; i ++) {
                var current = data.Posts[i];
                console.log(current);


                var postRow = $('<div id="' + current.Id + '" class="singlePostDiv hoverable" created="' + current.Created + '" rating="' + (current.Likes - current.Dislikes) + '" OwnerId =' + current.Owner.Id + ' style="opacity:0.9;border:1px solid #f9f9f9;box-shadow:3px 3px 10px #e2e2e2;" z-index="-1"></div>');

                var upperRow = $('<div class="row" style="margin-bottom:1em;"></div>');
                upperRow.append($('<div class="col-md-8"><h3 style="margin:0;display:inline;">' + current.Name + '</h3></div>'));
                upperRow.append($('<div class="col-md-4"><p style="margin:0;display:inline;font-size:0.7em;">' + current.CreatedString + '<p></div>'));
                postRow.append(upperRow);

                var imageDiv = $('<div style="margin:0;text-align:center;"></div>');
                var image = $('<img id="' + current.Id + '" class="hoverable clickable" src="/Content/PostPhotos/' + current.Icon + '" style="width:100%;max-width:25em;height:auto;" ></img>');
                imageDiv.append(image);
                postRow.append(imageDiv);


                var bottomRow = $('<div class="row" style="margin:1em 0 0 0;padding:0;"></div>');
                var bottomRowRating = $('<div class="col-md-3" style="margin:0.5em 0 0 0;padding:0;"></div>');

                if (localStorage.getItem('accessToken') != null && localStorage.getItem('accessToken') != '') {
                    if (localStorage.getItem('userPostLikes').includes(current.Id)) {
                        bottomRowRating.append($('<div style="margin:0;padding:0;display:inline;"><img id="' + current.Id + '" post_id="' + current.Id + '" class="hoverable clickable post-rating-positive"  src="/Content/AppPhotos/liked.png" style="width:auto;max-height:1.2em;margin:0 0 0 0;padding:0;" z-index="5"></img><p id="' + current.Id + '" class="post-rating-positive-value" style="display:inline;margin:0 0 0 0.3em;">' + current.Likes + '</p></div>'));
                        bottomRowRating.append($('<div style="margin:0;padding:0;display:inline;"><img id="' + current.Id + '" post_id="' + current.Id + '" class="hoverable clickable post-rating-negative"  src="/Content/AppPhotos/dislike.png" style="width:auto;max-height:1.2em;margin:0 0 0 0.5em;padding:0;" z-index="5" ></img><p id="' + current.Id + '" class="post-rating-negative-value" style="display:inline;margin:0 0 0 0.3em;">' + current.Dislikes + '</p></div>'));
                    }
                    else if (localStorage.getItem('userPostDislikes').includes(current.Id)) {
                        bottomRowRating.append($('<div style="margin:0;padding:0;display:inline;"><img id="' + current.Id + '" post_id="' + current.Id + '" class="hoverable clickable post-rating-positive"  src="/Content/AppPhotos/like.png" style="width:auto;max-height:1.2em;margin:0 0 0 0;padding:0;" z-index="5"></img><p id="' + current.Id + '" class="post-rating-positive-value" style="display:inline;margin:0 0 0 0.3em;">' + current.Likes + '</p></div>'));
                        bottomRowRating.append($('<div style="margin:0;padding:0;display:inline;"><img id="' + current.Id + '" post_id="' + current.Id + '" class="hoverable clickable post-rating-negative"  src="/Content/AppPhotos/disliked.png" style="width:auto;max-height:1.2em;margin:0 0 0 0.5em;padding:0;" z-index="5" ></img><p id="' + current.Id + '" class="post-rating-negative-value" style="display:inline;margin:0 0 0 0.3em;">' + current.Dislikes + '</p></div>'));
                    }
                    else {
                        bottomRowRating.append($('<div style="margin:0;padding:0;display:inline;"><img id="' + current.Id + '" post_id="' + current.Id + '" class="hoverable clickable post-rating-positive"  src="/Content/AppPhotos/like.png" style="width:auto;max-height:1.2em;margin:0 0 0 0;padding:0;" z-index="5"></img><p id="' + current.Id + '" class="post-rating-positive-value" style="display:inline;margin:0 0 0 0.3em;">' + current.Likes + '</p></div>'));
                        bottomRowRating.append($('<div style="margin:0;padding:0;display:inline;"><img id="' + current.Id + '" post_id="' + current.Id + '" class="hoverable clickable post-rating-negative"  src="/Content/AppPhotos/dislike.png" style="width:auto;max-height:1.2em;margin:0 0 0 0.5em;padding:0;" z-index="5" ></img><p id="' + current.Id + '" class="post-rating-negative-value" style="display:inline;margin:0 0 0 0.3em;">' + current.Dislikes + '</p></div>'));
                    }
                }
                else {
                    bottomRowRating.append($('<div style="margin:0;padding:0;display:inline;"><img id="' + current.Id + '" post_id="' + current.Id + '" class="hoverable clickable post-rating-positive"  src="/Content/AppPhotos/like.png" style="width:auto;max-height:1.2em;margin:0 0 0 0;padding:0;" z-index="5"></img><p id="' + current.Id + '" class="post-rating-positive-value" style="display:inline;margin:0 0 0 0.3em;">' + current.Likes + '</p></div>'));
                    bottomRowRating.append($('<div style="margin:0;padding:0;display:inline;"><img id="' + current.Id + '" post_id="' + current.Id + '" class="hoverable clickable post-rating-negative"  src="/Content/AppPhotos/dislike.png" style="width:auto;max-height:1.2em;margin:0 0 0 0.5em;padding:0;" z-index="5" ></img><p id="' + current.Id + '" class="post-rating-negative-value" style="display:inline;margin:0 0 0 0.3em;">' + current.Dislikes + '</p></div>'));
                }
                
                bottomRow.append(bottomRowRating);

                
                bottomRow.append($('<div class="col-md-6" style="margin:0;padding:0;text-align:center;"><Button post_id="' + current.Id + '" class="btn btn-default btn-sm comment-button" style="margin:0;width:100%;border-color:#f6f6f6;"><span class="glyphicon glyphicon-triangle-bottom" style="font-size:0.5em;"/> View comments <span class="glyphicon glyphicon-triangle-bottom" style="font-size:0.5em;"/></Button></div>'));

                bottomRow.append($('<div class="col-md-3" style="margin:0;padding:0;text-align:right;"><img author_id="' + current.Owner.Id + '" class="hoverable clickable" src="/Content/UserPhotos/' + current.Owner.Icon + '" style="margin:0;width:100%;max-width:2em;border-radius:50%;display:inline;" title="' + current.Owner.UserName + '"></img><div class="col-md-4">'));


                postRow.append(bottomRow);
                $('#PostsContainer').append(postRow);
            }
        },
        error: function (e) {
            console.log(e);
        }

    });
}

$(document).on('click', '.comment-button', function (e) {
    e.preventDefault();
    var postId = $(this).attr('post_id');
    if ($(this)[0].textContent.toString().includes('Hide')) {
        $(this)[0].innerHTML = '<span class="glyphicon glyphicon-triangle-bottom" style="font-size:0.5em;margin-bottom:1em;"></span> <p style="display:inline;font-family:Arial;font-size:1em;">View comments</p> <span class="glyphicon glyphicon-triangle-bottom" style="font-size:0.5em;margin-top:-2em;"></span>';
        $('#' + postId + '.commentSection').remove();
    } else {
        PopulateComments(postId);
        $(this)[0].innerHTML = '<span class="glyphicon glyphicon-triangle-top" style="font-size:0.5em;margin-bottom:1em;"></span> <p style="display:inline;font-family:Arial;font-size:1em;">Hide comments</p> <span class="glyphicon glyphicon-triangle-top" style="font-size:0.5em;margin-top:-2em;"></span>';
    }
    
   
    console.log($(this)[0]);
    
});


$(document).on('click', '#createPostButtonSubmit', function (e) {
    e.preventDefault();
    //var formData = new FormData();
    //console.log($('#createPostInputIcon'));
    //formData.append('imageFile', $('#createPostInputIcon')[0].files[0]);
    //formData.append('postName', $('#createPostInputName').val());
    //$.ajax({
    //    url: '/api/Post/Create',
    //    type: 'post',
        
    //    processData: false,
    //    data: formData,
    //    success: function (e) {
    //        console.log('success');
    //    },
    //    error: function (e) {
    //        console.log(e);
    //    }

    //});
    var secFormdata = new FormData();
    

    var file = document.getElementById('createPostInputIcon').files[0];
    if (file) {
        // create reader
        var reader = new FileReader();
        reader.readAsText(file);
        reader.onload = function (e) {
            console.log(e);
            // browser completed reading file - display it
            //alert(e.target.result);
            console.log($('createPostInputIcon'));
            secFormdata.append('imageFile', $('#createPostInputIcon')[0].files[0]);
            secFormdata.append('postName', $('#createPostInputName').val());
            $.ajax({
                url: '/api/Post/Create',
                type: 'post',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
                },
                
                cache: false,
                contentType : false,
                processData: false,
                data: secFormdata,
                success: function (e) {
                    location.reload();
                },
                error: function (e) {
                    console.log(e);
                }

            });
        };
    } 

    

    
});