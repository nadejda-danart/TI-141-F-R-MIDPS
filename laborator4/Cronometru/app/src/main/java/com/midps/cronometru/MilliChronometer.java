package com.midps.cronometru;

import android.content.Context;
import android.os.Handler;
import android.os.Message;
import android.os.SystemClock;
import android.util.AttributeSet;
import android.widget.TextView;
import java.text.DecimalFormat;

public class MilliChronometer extends TextView {
    @SuppressWarnings("unused")
    private static final String TAG = "Chronometer";
    private static final int TICK_WHAT = 2;

    private long previous;
    private long timeElapsed;
    private boolean mVisible;
    private boolean mStarted;
    private boolean mRunning;

    public MilliChronometer(Context context) {
        this (context, null, 0);
    }

    public MilliChronometer(Context context, AttributeSet attrs) {
        this (context, attrs, 0);
    }

    public MilliChronometer(Context context, AttributeSet attrs, int defStyle) {
        super (context, attrs, defStyle);

        init();
    }

    private void init() {
        previous = 0;
        updateText(previous);
    }

    public void start() {
        previous = SystemClock.elapsedRealtime();
        mStarted = true;
        updateRunning();
    }

    public void stop() {
        mStarted = false;
        updateRunning();
    }

    public void reset()
    {
        mStarted = false;
        timeElapsed = 0;
        updateRunning();
        updateText(previous);
    }

    public boolean isStarted()
    {
        return mStarted;
    }

    public String getTime()
    {
        DecimalFormat df = new DecimalFormat("00");
        DecimalFormat mf = new DecimalFormat("000");

        long hours = timeElapsed / (3600 * 1000);
        long remaining = timeElapsed % (3600 * 1000);

        long minutes = remaining / (60 * 1000);
        remaining = remaining % (60 * 1000);

        long seconds = remaining / 1000;
        remaining = remaining % 1000;

        long milliseconds = remaining;

        StringBuilder text = new StringBuilder();

        if (hours > 0) {
            text.append(df.format(hours));
            text.append(":");
        }

        text.append(df.format(minutes));
        text.append(":");
        text.append(df.format(seconds));
        text.append(":");
        text.append(mf.format(milliseconds));

        return text.toString();
    }

    @Override
    protected void onDetachedFromWindow() {
        super.onDetachedFromWindow();
        mVisible = false;
        updateRunning();
    }

    @Override
    protected void onWindowVisibilityChanged(int visibility) {
        super.onWindowVisibilityChanged(visibility);
        mVisible = visibility == VISIBLE;
        updateRunning();
    }

    private synchronized void updateText(long now) {
        timeElapsed += now - previous;
        previous = now;

        setText(getTime());
    }

    private void updateRunning() {
        boolean running = mVisible && mStarted;
        if (running != mRunning) {
            if (running) {
                updateText(SystemClock.elapsedRealtime());
                mHandler.sendMessageDelayed(Message.obtain(mHandler, TICK_WHAT), 100);
            } else {
                mHandler.removeMessages(TICK_WHAT);
            }
            mRunning = running;
        }
    }

    private Handler mHandler = new Handler() {
        public void handleMessage(Message m) {
            if (mRunning) {
                updateText(SystemClock.elapsedRealtime());
                sendMessageDelayed(Message.obtain(this , TICK_WHAT),
                        100);
            }
        }
    };
}
