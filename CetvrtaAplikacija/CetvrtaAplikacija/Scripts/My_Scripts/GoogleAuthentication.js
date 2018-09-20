$('#loginGoogle').on('click', function (e) {
    //api/Account/ExternalLogins?returnUrl=%2F&generateState=true
    window.location.href = '/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A58743%2F&state=hDOVrwZt2PAAZIEo-hwtWncB5KL571Rb7R7FcIIW4Wc1';

});


function getAccessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                isUserRegistered(accessToken);
            }
        }
    }
}

function isUserRegistered(accessToken) {
    $.ajax({
        url: '/api/Account/UserInfo',
        headers: {
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (e) {
            console.log('func isuserreg');
            console.log(e);
            if (e.HasRegistered) {
                localStorage.setItem('accessToken', accessToken);
                localStorage.setItem('userName', e.Email);
                window.location.href = '/';
            } else {
                signupExternalUser(accessToken);
            }
        },
        error: function (e) {
            console.log('puko 1');
        }
    });
}


function signupExternalUser(accessToken) {
    $.ajax({
        type: 'post',
        url: '/api/Account/RegisterExternal',
        headers: {
            'content-type': 'application/json',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (e) {
            console.log('func signup exernal');
            console.log(e);
            window.location.href = '/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A58743%2F&state=hDOVrwZt2PAAZIEo-hwtWncB5KL571Rb7R7FcIIW4Wc1';
        },
        error: function (e) {
            console.log('puko 2');
        }
    });
}