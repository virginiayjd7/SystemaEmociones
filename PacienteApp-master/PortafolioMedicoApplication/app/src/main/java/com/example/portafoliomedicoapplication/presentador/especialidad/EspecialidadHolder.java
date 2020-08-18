package com.example.portafoliomedicoapplication.presentador.especialidad;

import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.portafoliomedicoapplication.R;

public class EspecialidadHolder extends RecyclerView.ViewHolder {

    public TextView txvName;
    public ImageView imgPhoto;
    public LinearLayout elementoEspecialidad;
    public EspecialidadHolder(@NonNull View itemView) {
        super(itemView);
        txvName = itemView.findViewById(R.id.name_specialty_row);
        imgPhoto = itemView.findViewById(R.id.img_specialty_row);
        elementoEspecialidad = itemView.findViewById(R.id.elemento_especialidad);
    }


}
