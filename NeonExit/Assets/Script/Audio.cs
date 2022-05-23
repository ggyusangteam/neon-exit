using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public float time;
    public AudioSource _audioSource;
    // Start is called before the first frame update
    public static float[] _spectrumdata = new float[512];     // ����� �ҽ��� ���ļ��� ���� �迭 
    public static float[] _freqBand = new float[8];   //spectrumdata�� ����� ���ļ��� �ٽ��ѹ� ���ǹ��� ������ ���� �迭 
    public static float[] _bandBuffer = new float[8]; // 
    float[] _bufferDecrease = new float[8];
    void Start()
    {
        _audioSource.PlayDelayed(time);
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
    }
    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }
            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }




    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_spectrumdata, 0, FFTWindow.Blackman);
        // ����� �ҽ����� ����Ʈ�� �����͸� ������(�����迭,ä��,���Ǫ������ȯ���)
    }

    /*
     *22050 / 512 = 43hertz per sample
     * 
     * ����� ������ �ִ� ���ļ� = 22050 
     * ���� ������ 
     20 - 60 hertz  Sub Bass  ���� ���ϴ� ���̽� 
     60 - 250 hertz Bass 
     250 - 500 hertz Low Midrange
     500 - 2000 hertz  Midrange
     2000 - 4000 hertz Upper Midrange
     4000 - 6000 hertz Presence
     6000 - 20000 hertz Brilliance
     * 
     * 
     * 
     0 - 2 = 86 hertz
     1 - 4 = 172 hertz - 87-258
     2 - 8 = 344 hertz - 259-602
     3 - 16 = 688 hertz - 603-1290
     4 - 32 = 1376 hertz - 1291-2666
     5 - 64 = 2752 hertz - 2667-5418
     6 - 128 = 5504 hertz - 5419-10922
     7 - 256 = 11008 hertz - 10923-21930
     * ������ 510
   */
    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)   //8���� ä��
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;   //pow(2,2) = 2*2 =4 , pow (5,3) = 5*5*5 = 125
            /*
             *  sampleCount(i=0)=2
             *  sampleCount(i=1)=4
             *  sampleCount(i=2)=8
             *  sampleCount(i=3)=16
             *  sampleCount(i=4)=32
             *  sampleCount(i=5)=64
             *  sampleCount(i=6)=128
             *  sampleCount(i=7)=256
             */

            if (i == 7)
            {
                sampleCount += 2;

            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += _spectrumdata[count] * (count + 1);  //count +1 �� ���ϴ� ������ �߸𸣰���
                count++;
            }
            average /= count;
            _freqBand[i] = average * 10;
        }


    }
}
