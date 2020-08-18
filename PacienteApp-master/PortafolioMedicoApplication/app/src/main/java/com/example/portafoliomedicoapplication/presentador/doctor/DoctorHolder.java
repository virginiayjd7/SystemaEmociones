package com.example.portafoliomedicoapplication.presentador.doctor;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.cardview.widget.CardView;
import androidx.recyclerview.widget.RecyclerView;

import com.example.portafoliomedicoapplication.R;
import com.example.portafoliomedicoapplication.interfaces.DoctorInterface;
import com.example.portafoliomedicoapplication.interfaces.UsuarioInterface;
import com.example.portafoliomedicoapplication.modelo.UsuarioModelo;

import org.w3c.dom.Text;

public class DoctorHolder extends RecyclerView.ViewHolder {

    public ImageView imgProfile;
    public TextView txvFullName;
    public TextView txvPrice;
    public TextView txvDistance;
    public CardView cardView;
    public TextView txvEspecialidad;
    private UsuarioInterface.RowListener rowListener;

    public DoctorHolder(@NonNull View itemView , UsuarioInterface.RowListener rowListener) {
        super(itemView);
        this.rowListener = rowListener;
        imgProfile = itemView.findViewById(R.id.img_profile_doctor_row);
        txvFullName = itemView.findViewById(R.id.fullname_doctor_row);
        txvPrice = itemView.findViewById(R.id.price_doctor_row);
        txvDistance = itemView.findViewById(R.id.distance_doctor_row);
        cardView = itemView.findViewById(R.id.card_container_doctor_row);
        txvEspecialidad = itemView.findViewById(R.id.specialty_doctor_row);
    }
//////////
    void bindListenerClickRow(CardView cardContainer, final UsuarioModelo usuarioDoctor){
        cardContainer.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                rowListener.onClickDoctorRow(usuarioDoctor);
            }
        });
    }
}
