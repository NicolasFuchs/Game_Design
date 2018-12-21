using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private enum MusicState {STOPPED, PLAYING}
    public enum Command { PLAY, STOP }
    public enum WindLevel { NUL, BREEZE, LIGHT, MEDIUM, EXTREME }
    public enum Heartbeat { NUL, SLOW, MEDIUM, QUICK}
    public enum Water { NUL, STILL, SWIM, SPLASHLIGHT, SPLASHHEAVY }
    public enum SFX { NONE, ICEORCA, BREATH }

    private MusicState musicState = MusicState.STOPPED;
    private WindLevel currentWind = WindLevel.NUL;
    private Heartbeat currentHeartbeat = Heartbeat.NUL;
    private Water currentWater = Water.NUL;
    
    // Audio vars
    private AudioSource audioAmbiant;
    public float ambiantVolume = 1.0f;

    private AudioSource audioMusic;
    public float musicVolume = 1.0f;

    private AudioSource audioSFX;
    public float sfxVolume = 1.0f;

    private AudioSource audioHeartbeat;
    public float heartbeatVolume = 1.0f;

    private AudioSource audioWater;
    public float waterVolume = 1.0f;

    private AudioClip[] wind;
    private AudioClip[] music;
    private AudioClip iceOrca;
    private AudioClip iceNormal;
    private AudioClip swimMoving;
    private AudioClip swimStill;
    private AudioClip splashLight;
    private AudioClip splashHeavy;
    private AudioClip[] heartbeat;
    private AudioClip breath;

    private int currentTrack = 0;
    private int nbrTracks = 5;
    private int windTrack = 0;

    void Start()
    {
        // Musics
        music = new AudioClip[nbrTracks];
        music[0] = Resources.Load<AudioClip>("Sound/Musics/Music_Cold_As_Ice");
        music[1] = Resources.Load<AudioClip>("Sound/Musics/Music_DangerZone");
        music[2] = Resources.Load<AudioClip>("Sound/Musics/Music_Sacrifice");
        music[3] = Resources.Load<AudioClip>("Sound/Musics/Music_StormWatch");
        music[4] = Resources.Load<AudioClip>("Sound/Musics/Music_Suspended");

        // Ambiant Sounds
        wind = new AudioClip[5];
        //wind[0] = Resources.Load<AudioClip>("Sound/Wind/Blizzard/blizzard_wind_howling_extreme");
        wind[0] = Resources.Load<AudioClip>("Sound/Wind/Ambiant/Wind_Light_Ambiant");
        wind[1] = Resources.Load<AudioClip>("Sound/Wind/Ambiant/Wind_Heavy_Ambiant");
        wind[2] = Resources.Load<AudioClip>("Sound/Wind/Blizzard/blizzard_wind_howling_normal");
        wind[3] = Resources.Load<AudioClip>("Sound/Wind/Blizzard/blizzard_wind_howling_medium");
        wind[4] = Resources.Load<AudioClip>("Sound/Wind/Blizzard/blizzard_wind_howling_extreme");

        // Heartbeat
        heartbeat = new AudioClip[3];
        heartbeat[0] = Resources.Load<AudioClip>("Sound/HeartBeat/HeartBeat_Slow");
        heartbeat[1] = Resources.Load<AudioClip>("Sound/HeartBeat/HeartBeat_Quick");
        heartbeat[2] = Resources.Load<AudioClip>("Sound/HeartBeat/HeartBeat_Quick_Heavy");

        // SFX sounds
        iceOrca = Resources.Load<AudioClip>("Sound/IceCrack/Ice_Crack_by_Walking");
        iceNormal = Resources.Load<AudioClip>("Sound/IceCrack/Ice_Crack_by_Orca");
        swimMoving = Resources.Load<AudioClip>("Sound/Water/Swim/Swimming_Moving");
        swimStill = Resources.Load<AudioClip>("Sound/Water/Swim/Swimming_Standing_Still");
        splashLight = Resources.Load<AudioClip>("Sound/Water/Splash/Splash_Light");
        splashHeavy = Resources.Load<AudioClip>("Sound/Water/Splash/Splash_Heavy");
        breath = Resources.Load<AudioClip>("Sound/Damages/Breath_Damages");

        // Ambiant Soundtrack
        audioAmbiant = gameObject.AddComponent<AudioSource>();
        audioAmbiant.volume = ambiantVolume;

        // Background Music
        audioMusic = gameObject.AddComponent<AudioSource>();
        audioMusic.loop = true;
        audioMusic.volume = musicVolume;

        // SFX
        audioSFX = gameObject.AddComponent<AudioSource>();
        audioSFX.volume = sfxVolume;

        // Heartbeat
        audioHeartbeat = gameObject.AddComponent<AudioSource>();
        audioHeartbeat.loop = true;
        audioHeartbeat.volume = heartbeatVolume;

        // Water
        audioWater = gameObject.AddComponent<AudioSource>();
        audioWater.volume = waterVolume;
    }

    void FixedUpdate()
    {
        // Ambiant Breeze
        if(currentWind == WindLevel.BREEZE && !audioAmbiant.isPlaying)
        {
            windTrack = (windTrack + 1) % 2;
            audioAmbiant.PlayOneShot(wind[windTrack]);
        }
        

        // Music track
        if (musicState==MusicState.PLAYING && !audioMusic.isPlaying)
        {
            currentTrack = (currentTrack + 1) % nbrTracks;
            audioMusic.PlayOneShot(music[currentTrack]);
        }

        if(!audioWater.isPlaying)
        {
            if(currentWater == Water.STILL) audioWater.PlayOneShot(swimStill);
            else if (currentWater == Water.SWIM) audioWater.PlayOneShot(swimMoving);
        }

    }

    public void SetAmbiantVolume(float volume)
    {
        if (volume > 1.0f) volume = 1.0f;
        else if (volume < 0.0f) volume = 0.0f;
        ambiantVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        if (volume > 1.0f) volume = 1.0f;
        else if (volume < 0.0f) volume = 0.0f;
        musicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        if (volume > 1.0f) volume = 1.0f;
        else if (volume < 0.0f) volume = 0.0f;
        sfxVolume = volume;
    }

    public void SetHeartbeatVolume(float volume)
    {
        if (volume > 1.0f) volume = 1.0f;
        else if (volume < 0.0f) volume = 0.0f;
        heartbeatVolume = volume;
    }

    public void SetWaterVolume(float volume)
    {
        if (volume > 1.0f) volume = 1.0f;
        else if (volume < 0.0f) volume = 0.0f;
        waterVolume = volume;
    }

    public void SetHeartbeat(Heartbeat level)
    {
        if (audioHeartbeat.isPlaying) audioHeartbeat.Stop();
        switch (level)
        {
            case Heartbeat.NUL: break;
            case Heartbeat.SLOW: audioHeartbeat.clip = heartbeat[0]; audioHeartbeat.Play(); break;
            case Heartbeat.MEDIUM: audioHeartbeat.clip = heartbeat[1]; audioHeartbeat.Play(); break;
            case Heartbeat.QUICK: audioHeartbeat.clip = heartbeat[2]; audioHeartbeat.Play(); break;
            default: break;
        }
        currentHeartbeat = level;
    }

    public void SetWindLevel(WindLevel level)
    {
        if (audioAmbiant.isPlaying) audioAmbiant.Stop();
        
        switch (level)
        {
            case WindLevel.NUL: break;
            case WindLevel.BREEZE: audioAmbiant.clip = wind[windTrack]; audioAmbiant.Play(); break;
            case WindLevel.LIGHT: audioAmbiant.clip = wind[2]; audioAmbiant.Play(); break;
            case WindLevel.MEDIUM: audioAmbiant.clip = wind[3]; audioAmbiant.Play(); break;
            case WindLevel.EXTREME: audioAmbiant.clip = wind[4]; audioAmbiant.Play(); break;
            default:break;
        }
        currentWind = level;
    }

    public void SetWater(Water level)
    {
        if (audioWater.isPlaying && (currentWater == Water.STILL || currentWater == Water.SWIM)) audioWater.Stop();
        switch (level)
        {
            case Water.NUL: audioWater.Stop(); break;
            case Water.SPLASHHEAVY: audioWater.PlayOneShot(splashHeavy); break;
            case Water.SPLASHLIGHT: audioWater.PlayOneShot(splashLight); break;
            case Water.STILL: audioWater.PlayOneShot(swimStill); break;
            case Water.SWIM: audioWater.PlayOneShot(swimMoving); break;
            default: break;
        }
        currentWater = level;
    }

    public void SetMusicState(Command command)
    {
        switch (command)
        {
            case Command.PLAY: musicState = MusicState.PLAYING; break;
            case Command.STOP: musicState = MusicState.STOPPED; break;
            default: break;
        }
    }

    public void PlaySFX(SFX soundEffect)
    {
        switch (soundEffect)
        {
            case SFX.BREATH: audioSFX.PlayOneShot(breath); break;
            case SFX.ICEORCA: audioSFX.PlayOneShot(iceOrca); break;
            case SFX.NONE: audioSFX.Stop(); break;
            default: break;
        }
    }

    public bool isPlayingWater()
    {
        return currentWater != Water.NUL;
    }

    public bool isPlayingMusic()
    {
        return musicState == MusicState.PLAYING;
    }

    public bool isPlayingHeartbeat()
    {
        return currentHeartbeat != Heartbeat.NUL;
    }

    public bool isPlayingAmbiant()
    {
        return currentWind != WindLevel.NUL;
    }

	public bool isPlayingSFX()
    {
		return audioSFX.isPlaying;
	}
}
