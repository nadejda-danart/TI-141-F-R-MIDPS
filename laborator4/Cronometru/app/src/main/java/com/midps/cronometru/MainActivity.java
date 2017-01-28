package com.midps.cronometru;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    private List<String> laps;
    private ArrayAdapter<String> adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        laps = new ArrayList<>();
        adapter = new LapAdapter(getApplicationContext(), R.layout.lap_row, R.id.lapRowText, laps);
        getLapList().setAdapter(adapter);
    }

    public void startChronometer(View view) {
        if (getChronometer().isStarted()) {
            getChronometer().stop();
            getStartButton().setText(R.string.start);
            getLapButton().setEnabled(false);
            getResetButton().setEnabled(true);
        } else {
            getChronometer().start();
            getStartButton().setText(R.string.stop);
            getLapButton().setEnabled(true);
            getResetButton().setEnabled(false);
        }
    }

    public void saveLap(View view) {
        laps.add(0, getChronometer().getTime());
        adapter.notifyDataSetChanged();
    }

    public void reset(View view) {
        getChronometer().reset();
        laps.clear();
        adapter.notifyDataSetChanged();
    }

    private MilliChronometer getChronometer() {
        return (MilliChronometer)findViewById(R.id.chronometer);
    }

    private Button getStartButton() {
        return find(R.id.buttonStart);
    }

    private Button getLapButton() {
        return find(R.id.buttonLap);
    }

    private Button getResetButton() {
        return find(R.id.buttonReset);
    }

    private ListView getLapList()
    {
        return find(R.id.lapList);
    }

    @SuppressWarnings("unchecked")
    private <T> T find(int id) {
        return (T)findViewById(id);
    }
}
