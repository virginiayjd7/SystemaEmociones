package com.example.portafoliomedicoapplication;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.drawable.AnimationDrawable;
import android.os.Bundle;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.RelativeLayout;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.SignInButton;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthCredential;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.auth.GoogleAuthProvider;

public class Login extends AppCompatActivity implements GoogleApiClient.OnConnectionFailedListener {

    AnimationDrawable drawableAnimation;

    private GoogleApiClient mGoogleApiClient;
    private SignInButton logingoogle;

    private FirebaseAuth firebaseAuth;
    private FirebaseAuth.AuthStateListener firebaseAuthListener;

    private ProgressBar progressBar;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        /* ANIMACIÓN DEL LOGIN */

        /*RelativeLayout relativeLayout = (RelativeLayout) findViewById(R.id.root_layout);
        relativeLayout.setBackgroundResource(R.drawable.gradient_animation);

        drawableAnimation = (AnimationDrawable) relativeLayout.getBackground();
        drawableAnimation.setEnterFadeDuration(6);
        drawableAnimation.setExitFadeDuration(5000);
        drawableAnimation.start();

        /** AUTENTICACIÓN CON GOOGLE */

        logingoogle = (SignInButton) findViewById(R.id.signInButton);

        logingoogle.setSize(SignInButton.SIZE_WIDE);

        logingoogle.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                signInWithGoogle();

            }
        });

        firebaseAuth = FirebaseAuth.getInstance();
        firebaseAuthListener = new FirebaseAuth.AuthStateListener() {
            @Override
            public void onAuthStateChanged(@NonNull FirebaseAuth firebaseAuth) {
                FirebaseUser user = firebaseAuth.getCurrentUser();
                if (user != null)
                {
                    IrPerfilUsuario();
                }
            }
        };

        progressBar = (ProgressBar) findViewById(R.id.progressBar);

    }

    @Override
    protected void onStart() {
        super.onStart();

        firebaseAuth.addAuthStateListener(firebaseAuthListener);
    }

    private static final int RC_SIGN_IN = 9001;

    private void signInWithGoogle() {
        if(mGoogleApiClient != null) {
            mGoogleApiClient.disconnect();
        }

        GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
                .requestIdToken(getString(R.string.common_google_play_services_unsupported_text))
                .requestEmail()
                .build();

        mGoogleApiClient = new GoogleApiClient.Builder(this)
                .addApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .build();

        final Intent signInIntent = Auth.GoogleSignInApi.getSignInIntent(mGoogleApiClient);
        startActivityForResult(signInIntent, RC_SIGN_IN);
    }


    public void IrPerfilUsuario (){
        Intent intent = new Intent(this, PrincipalActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_CLEAR_TASK | Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(intent);
    }


    public void EnviarMenu (View view){
        Intent intent = new Intent(this, PrincipalActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_CLEAR_TASK | Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(intent);
    }


    @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {

    }


    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if (requestCode == RC_SIGN_IN) {
            GoogleSignInResult result = Auth.GoogleSignInApi.getSignInResultFromIntent(data);

            if (result.isSuccess()) {
                final GoogleApiClient client = mGoogleApiClient;
                GoogleSignInAccount profile = result.getSignInAccount();

                // get profile information
                String name = "";
                String email = "";
                String uriPicture = "";
                if (profile.getDisplayName() != null) {
                    name = profile.getDisplayName();
                }
                if (profile.getEmail() != null) {
                    email = profile.getEmail();
                }
                if (profile.getPhotoUrl() != null) {
                    uriPicture = profile.getPhotoUrl().toString();
                }
                // guardar el perfil de información
                SharedPreferences prefs = getSharedPreferences("com.misuperapp.app", Context.MODE_PRIVATE);
                prefs.edit().putString("com.misuperapp.app.nombre", name).apply();
                prefs.edit().putString("com.misuperapp.app.email", email).apply();
                prefs.edit().putString("com.misuperapp.app.uriPicture", uriPicture).apply();
                // redirect to map screen

                firebaseAuthWithGoogle(result.getSignInAccount());

                //startActivity(new Intent(this, PerfilActivity.class));
            } else {
                // Otros result de actividades de inicio de session como facebook o twitter
            }


        }
    }

    private void firebaseAuthWithGoogle(GoogleSignInAccount signInAccount) {

        progressBar.setVisibility(View.VISIBLE);
        logingoogle.setVisibility(View.GONE);

        AuthCredential credential = GoogleAuthProvider.getCredential(signInAccount.getIdToken(),null);
        firebaseAuth.signInWithCredential(credential).addOnCompleteListener(this, new OnCompleteListener<AuthResult>() {
            @Override
            public void onComplete(@NonNull Task<AuthResult> task) {

                progressBar.setVisibility(View.GONE);
                logingoogle.setVisibility(View.VISIBLE);
                if(task.isSuccessful())
                {
                    //Toast.makeText(MainActivity.this,"No se pudo autenticar con Firebase", Toast.LENGTH_SHORT).show();
                }

            }
        });
    }

    @Override
    protected void onStop() {
        super.onStop();

        if(firebaseAuthListener != null)
        {
            firebaseAuth.removeAuthStateListener(firebaseAuthListener);
        }
    }
}
