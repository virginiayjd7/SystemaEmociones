package com.example.portafoliomedicoapplication.vista.reserva;

import android.os.Bundle;

import androidx.annotation.Nullable;
import androidx.appcompat.widget.AppCompatImageButton;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentPagerAdapter;
import androidx.viewpager.widget.ViewPager;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.example.portafoliomedicoapplication.R;
import com.example.portafoliomedicoapplication.helpers.CustomViewPager;
import com.example.portafoliomedicoapplication.interfaces.ReservaInterface;
import com.example.portafoliomedicoapplication.modelo.UsuarioModelo;
import com.google.android.material.tabs.TabLayout;

/**
 * A simple {@link Fragment} subclass.
 */
public class ProcessAppointmentFragment extends DialogFragment {

    private AppCompatImageButton btnClose;
    private UsuarioModelo Ojex;
    private CustomViewPager viewPager;
    private Button button;
    private SliderPagerAppointmentAdapter adapter;

    public ProcessAppointmentFragment() {
        // Required empty public constructor
    }

    public ProcessAppointmentFragment(UsuarioModelo objex) {
        // Required empty public constructor
        Ojex=objex;///////save
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setStyle(DialogFragment.STYLE_NORMAL,R.style.FullscreenDialogTheme);


    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_process_appointment, container, false);
        btnClose = view.findViewById(R.id.btn_close_dialog_appointment);
        btnClose.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                closeDialog();
            }
        });


        ReservaInterface.VistaDialogReserva inter = new ReservaInterface.VistaDialogReserva() {
            @Override
            public void reserveAndCloseDialog() {
                closeDialog();
            }

            @Override
            public void nextItemPage() {
                viewPager.setCurrentItem(viewPager.getCurrentItem()+1);
            }
        };
        // bind views
        viewPager = view.findViewById(R.id.pagerIntroSlider);
        viewPager.setPagingEnabled(false);
        TabLayout tabLayout = view.findViewById(R.id.tabs);
        button = view.findViewById(R.id.button);

        // init slider pager adapter
        adapter = new SliderPagerAppointmentAdapter(getChildFragmentManager(),
                FragmentPagerAdapter.BEHAVIOR_RESUME_ONLY_CURRENT_FRAGMENT,inter);

        //SETEAR USUARIO DOCTOR
        adapter.pasarParametroUsuarioDoctor(Ojex);
        // set adapter
        viewPager.setAdapter(adapter);
        // set dot indicators
        tabLayout.setupWithViewPager(viewPager);

        // make status bar transparent
//        changeStatusBarColor();

        button.setOnClickListener(new View.OnClickListener() {
            @Override public void onClick(View view) {
                nextOrPrevPage(viewPager.getCurrentItem());
            }
        });

        /**
         * Add a listener that will be invoked whenever the page changes
         * or is incrementally scrolled
         */
        viewPager.addOnPageChangeListener(new ViewPager.OnPageChangeListener() {
            @Override
            public void onPageScrolled(int position, float positionOffset, int positionOffsetPixels) {
                
            }

            @Override public void onPageSelected(int position) {
                Log.d("GA",""+position);
                if(position == 0){
                    button.setText("continuar");
                }

                if(position == 1){
                    button.setText("regresar");
                }
                if (position == adapter.getCount() - 1) {
                    button.setText("confirmar reserva");
                }
            }

            @Override public void onPageScrollStateChanged(int state) {

            }
        });

        return view;
    }

    private void closeDialog(){
        this.dismiss();
    }

    private void nextOrPrevPage(int currentItem){
        switch(currentItem){
            case 0:
                viewPager.setCurrentItem(currentItem + 1);
                break;
            case 1 :
                viewPager.setCurrentItem(currentItem - 1);
                break;
            case 2 :
                closeDialog();
                break;
            default:
                break;
        }
    }
}

