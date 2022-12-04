using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : SubBassInteraction
{
	public Material buildingMaterial;
	public Material buildingMaterialFirst;
	public Material buildingMaterialSecond;
	public Material GlassMaterial;

	public bool isOptimize = false;


	public float lerpTime;

	public float buildingR;
	public float buildingG;
	public float buildingB;

	public float NeonR;
	public float NeonG;
	public float NeonB;

	public float GlassR;
	public float GlassG;
	public float GlassB;

	public float buildingIntensity;
	public float buildingNeonIntensity;

	public float buildingGlassIntensity;



	private Color buildingMatFirst;
	private Color buildingMatSecond;

	private Color GlassMat;

	// Start is called before the first frame update
	void Start()
	{
		// ��Ƽ���� �ʱ갪 get

			buildingMatFirst = buildingMaterialFirst.GetColor("_Color");
			buildingMatSecond = buildingMaterialSecond.GetColor("_Color");
		


		GlassMat = GlassMaterial.GetColor("_Color");

	}

	// Update is called once per frame
	void Update()
	{
		if (Audio._freqBand[audioChannel] * threshold >= audioSensibility)
		{
			//���ļ� �Ӱ��� �Ѱ����� �� ����  
			Change_color();

		}
		else
		{
			//���� �÷� 
			/*
	
				*/
			if (!isOptimize)
			{
				Color buildingColor = buildingMaterial.GetColor("_Color");
				buildingMaterial.SetColor("_Color", Color.Lerp(buildingColor, new Color(0, 0, 0), Time.deltaTime * lerpTime));


				Color neonFirstColor = buildingMaterialFirst.GetColor("_Color");
				Color neonSecondColor = buildingMaterialSecond.GetColor("_Color");
				buildingMaterialFirst.SetColor("_Color", Color.Lerp(neonFirstColor, buildingMatFirst, Time.deltaTime * lerpTime));
				buildingMaterialSecond.SetColor("_Color", Color.Lerp(neonSecondColor, buildingMatSecond, Time.deltaTime * lerpTime));
			}
			Color glassColor = GlassMaterial.GetColor("_Color");

			//lerp�� ���� �÷��� õõ�� ���ư���
			/*

			*/

			GlassMaterial.SetColor("_Color", Color.Lerp(glassColor, GlassMat, Time.deltaTime * lerpTime));


		}
	}
	public void Change_color()
	{
		//������ �÷��� ����
		

			
			if(!isOptimize)
			{
			buildingMaterial.SetColor("_Color", new Color(buildingR, buildingG, buildingB) * buildingIntensity);

			buildingMaterialFirst.SetColor("_Color", new Color(NeonR, NeonG, NeonB) * buildingNeonIntensity);
			buildingMaterialSecond.SetColor("_Color", new Color(NeonR, NeonG, NeonB) * buildingNeonIntensity);
		}


		GlassMaterial.SetColor("_Color", new Color(GlassR, GlassG, GlassB) * buildingGlassIntensity);


	}

	private void OnApplicationQuit()
	{
		//����� ���� �÷��� ���� 
		
			buildingMaterial.SetColor("_Color", Color.black);
			
		buildingMaterialFirst.SetColor("_Color", buildingMatFirst);
		buildingMaterialSecond.SetColor("_Color", buildingMatSecond);

		GlassMaterial.SetColor("_Color", GlassMat);

	}
}
