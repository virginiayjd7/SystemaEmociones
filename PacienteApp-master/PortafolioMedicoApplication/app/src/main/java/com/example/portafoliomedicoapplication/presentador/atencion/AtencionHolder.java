package com.example.portafoliomedicoapplication.presentador.atencion;

import android.view.View;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.portafoliomedicoapplication.R;
import com.example.portafoliomedicoapplication.interfaces.AtencionInterface;
import com.google.android.material.button.MaterialButton;

public class AtencionHolder extends RecyclerView.ViewHolder {

    MaterialButton btnReserveAppointment;
    private AtencionInterface.RowListener rowListener;
    public AtencionHolder(@NonNull View itemView, AtencionInterface.RowListener rowListener) {
        super(itemView);
        btnReserveAppointment = itemView.findViewById(R.id.btn_reserve_appointment);
        this.rowListener = rowListener;
    }


    public void bindOnClickButtonReserveAppointment(MaterialButton btnToBind){
        btnToBind.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                rowListener.onItemClickReserveAppointment();
            }
        });
    }
}
