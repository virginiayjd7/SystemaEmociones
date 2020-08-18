package com.example.portafoliomedicoapplication.vista.reserva;

import android.os.Build;
import android.os.Bundle;
import android.os.Parcelable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import androidx.annotation.StringRes;
import androidx.fragment.app.Fragment;

import com.example.portafoliomedicoapplication.R;

import java.io.Serializable;

public class SliderItemFragment extends Fragment {

    private static final String ARG_POSITION = "slider-position";
    private static final String ARG_MODEL = "object_model";
    private static Serializable objAppointment;
    private int position;

    // prepare all background images arrays
//    @StringRes
//    private static final int[] BG_IMAGE = new int[] {
//            R.drawable.ic_bg_red, R.drawable.ic_bg_blue, R.drawable.ic_bg_green,
//            R.drawable.ic_bg_purple
//    };


    public SliderItemFragment() {
        // Required empty public constructor
    }


//    public static SliderItemFragment newInstance(int position) {
//        SliderItemFragment fragment = new SliderItemFragment();
//        objAppointment = new AppointmentModel();
//        Bundle args = new Bundle();
//        args.putInt(ARG_POSITION, position);
//        args.putSerializable(ARG_MODEL,objAppointment);
//        fragment.setArguments(args);
//        return fragment;
//    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            position = getArguments().getInt(ARG_POSITION);
        }
    }

//    @Override
//    public View onCreateView(LayoutInflater inflater, ViewGroup container,
//                             Bundle savedInstanceState) {
//        // Inflate the layout for this fragment
//        return inflater.inflate(R.layout.fragment_dashboard, container, false);
//    }

//    @RequiresApi(api = Build.VERSION_CODES.LOLLIPOP) @Override
//    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
//        super.onViewCreated(view, savedInstanceState);
//        // set page background
//        view.setBackground(requireActivity().getDrawable(BG_IMAGE[position]));
//
//    }
}
